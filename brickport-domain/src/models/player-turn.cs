using System;
using System.Collections.Generic;
using System.Linq;
using BrickPort.Domain.Models.PlayerActions;

namespace BrickPort.Domain.Models
{
    public interface IPlayerTurn
    {
        PlayerColor Player { get; }
        RollResult RollResult { get; }
        IReadOnlyList<IPlayerAction> Actions { get; }
        void Add(IPlayerAction action);
        void Undo();
        GameState Apply(GameState gameState);
    }

    public class PlayerTurn : IPlayerTurn
    {
        private readonly Stack<IPreRollAction> _preRollActions;
        private IRollAction _rollAction;
        protected readonly Stack<IPlayerAction> _postRollActions;

        public Guid Id { get; }
        public PlayerColor Player { get; }
        public RollResult RollResult => _rollAction.RollResult;
        public IReadOnlyList<IPreRollAction> PreRollActions => _preRollActions.ToList();
        public IReadOnlyList<IPlayerAction> PostRollActions => _postRollActions.ToList();
        public virtual IReadOnlyList<IPlayerAction> Actions
        {
            get 
            {
                var actions = _preRollActions.Cast<IPlayerAction>();
                if (_rollAction != null)
                    actions = actions.Concat(new IRollAction[] { _rollAction });
                return actions.Concat(_postRollActions).ToList();
            }
        } 

        public PlayerTurn(
            PlayerColor player, 
            IList<IPreRollAction> preRollActions = null,
            IRollAction roll = null,
            IList<IPlayerAction> postRollActions = null
        ) : this(Guid.NewGuid(), player, preRollActions, roll, postRollActions) { }
        
        public PlayerTurn(
            Guid id, 
            PlayerColor player, 
            IList<IPreRollAction> preRollActions = null,
            IRollAction roll = null,
            IList<IPlayerAction> postRollActions = null
        ) 
        {
            Id = id;
            Player = player;
            _preRollActions = new Stack<IPreRollAction>(preRollActions?.ToList() ?? new List<IPreRollAction>());
            _rollAction = roll;
            _postRollActions = new Stack<IPlayerAction>(postRollActions?.ToList() ?? new List<IPlayerAction>());
        }

        public GameState Apply(GameState gameState)
        {
            var newState = gameState.Clone();
            foreach(var playerAction in Actions)
                newState = playerAction.Apply(newState);
            return newState;
        }

        public virtual void UseDevelopmentCard(IUseDevelopmentCardAction playerAction)
        {
            // Ensure we are allowed to play this card
            var cardType = playerAction.CardType;
            var cardsPlayed = _preRollActions
                .Where(x => x is IUseDevelopmentCardAction)
                .Cast<IUseDevelopmentCardAction>()
                .Where(x => x.CardType.Equals(cardType) && x.PlayerColor.Equals(playerAction.PlayerColor))
                .Count();
            if (cardsPlayed > cardType.MaxPerTurn)
                throw new ActionLimitReachedException(playerAction);
            if (_rollAction != null) 
                _postRollActions.Push(playerAction);
            else
                _preRollActions.Push(playerAction);
        }

        public virtual void RollDice(IRollAction roll) 
        {
            if (_rollAction != null)
                throw new ActionLimitReachedException(roll);
            if (_postRollActions.Any()) 
                throw new InvalidOperationException("Cannot roll dice after post-roll actions have been recorded");
            _rollAction = roll;
        }

        public virtual void Add(IPlayerAction playerAction)
        {
            if (playerAction is IUseDevelopmentCardAction)
            {
                UseDevelopmentCard(playerAction as IUseDevelopmentCardAction);
                return;
            }
            if (playerAction is IRollAction)
            {
                RollDice(playerAction as IRollAction);
                return;
            }
            if (_rollAction == null) 
            {
                if (playerAction is IPreRollAction)
                    _preRollActions.Push(playerAction as IPreRollAction);
                else 
                    throw new InvalidOperationException($"Action ({playerAction.Description}) must be performed after roll");
            }
            else
                _postRollActions.Push(playerAction);
        }

        public virtual void Undo()
        {
            if (_postRollActions.Any())
                _postRollActions.Pop();
            else if (_rollAction != null)
                _rollAction = null;
            else if (_preRollActions.Any())
                _preRollActions.Pop();
            else
                throw new InvalidOperationException("No actions to undo");
        }

    }

    public class PlayerTurnWithSpecialBuildPhase : PlayerTurn
    {
        private readonly Stack<ISpecialBuildPhaseEligibleAction> _specialBuildActions;
        private bool SpecialBuildPhaseActive => _specialBuildActions.Any();

        public IReadOnlyList<ISpecialBuildPhaseEligibleAction> SpecialBuildPhaseActions => _specialBuildActions.ToList();
        public override IReadOnlyList<IPlayerAction> Actions => base.Actions
            .Concat(_specialBuildActions)
            .ToList();

        public PlayerTurnWithSpecialBuildPhase(
            PlayerColor player, 
            IList<IPreRollAction> preRollActions = null,
            IRollAction roll = null,
            IList<IPlayerAction> postRollActions = null,
            IList<ISpecialBuildPhaseEligibleAction> specialBuildActions = null
        ) : base(player, preRollActions, roll, postRollActions) =>
            _specialBuildActions = new Stack<ISpecialBuildPhaseEligibleAction>(specialBuildActions?.ToList() 
                ?? new List<ISpecialBuildPhaseEligibleAction>());
        
        public override void Add(IPlayerAction playerAction)
        {
            if (SpecialBuildPhaseActive && playerAction.PlayerColor.Equals(Player))
                throw new SpecialBuildPhaseException(playerAction);

            if (Player.Equals(playerAction.PlayerColor))
                base.Add(playerAction);
            else 
            {
                if (!(playerAction is ISpecialBuildPhaseEligibleAction)) 
                    throw new SpecialBuildPhaseException(playerAction);
                _specialBuildActions.Push(playerAction as ISpecialBuildPhaseEligibleAction);
            }
        }

        public override void Undo()
        {
            if (_specialBuildActions.Any())
                _specialBuildActions.Pop();
            else 
                base.Undo();
        }

        public override void UseDevelopmentCard(IUseDevelopmentCardAction playerAction)
        {
            if (_specialBuildActions.Any())
                throw new SpecialBuildPhaseException(playerAction);
            else
                base.UseDevelopmentCard(playerAction);
        }

    }

    public class ActionLimitReachedException : InvalidOperationException 
    {
        public ActionLimitReachedException(IPlayerAction playerAction) 
            : base($"Action ({playerAction.Description}) cannot be performed again this turn") { }
    }
    
    public class SpecialBuildPhaseException : InvalidOperationException 
    {
        public SpecialBuildPhaseException(IPlayerAction playerAction) 
            : base($"Action ({playerAction.Description}) cannot be performed during the special build phase") { }
    }

    public class PreGamePlayerTurnException : InvalidOperationException
    {
        public PreGamePlayerTurnException(IPlayerAction playerAction) 
            : base($"Action ({playerAction.Description}) may not be performed before game has started") { }
    }

    public class PreGamePlayerTurn : PlayerTurn
    {
        public PreGamePlayerTurn(PlayerColor player, IList<IPreGameAction> preGameActions = null) 
            : this(Guid.NewGuid(), player, preGameActions) { }
        public PreGamePlayerTurn(Guid id, PlayerColor player, IList<IPreGameAction> preGameActions = null) : base(id, player) 
        { 
            if (preGameActions?.Any() ?? false)
            {
                foreach(var preGameAction in preGameActions) 
                    Add(preGameAction);
            }
        }

        public override void Add(IPlayerAction playerAction)
        {
            if (!(playerAction is IPreGameAction))
                throw new PreGamePlayerTurnException(playerAction);
            _postRollActions.Push(playerAction as IPreGameAction);
        }
    }
}