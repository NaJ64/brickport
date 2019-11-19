using System;
using System.Collections.Generic;
using System.Linq;
using BrickPort.Domain.PlayerActions;

namespace BrickPort.Domain
{
    public class PlayerTurn
    {
        private readonly Stack<PlayerAction> _preRollActions;
        private RollDice _rollAction;
        private readonly Stack<PlayerAction> _postRollActions;

        public Guid Id { get; }
        public PlayerColor PlayerColor { get; }
        public RollResult RollResult => _rollAction.RollResult;
        public IReadOnlyList<PlayerAction> PreRollActions => _postRollActions.ToList();
        public IReadOnlyList<PlayerAction> PostRollActions => _postRollActions.ToList();
        public IReadOnlyList<PlayerAction> Actions => _preRollActions
            .Concat(new PlayerAction[] { _rollAction })
            .Concat(_postRollActions).ToList();

        public PlayerTurn(
            PlayerColor playerColor, 
            IList<PlayerAction> preRollActions = null,
            RollDice rollAction = null,
            IList<PlayerAction> postRollActions = null
        ) : this(Guid.NewGuid(), playerColor, preRollActions, rollAction, postRollActions) { }
        
        public PlayerTurn(
            Guid id, 
            PlayerColor playerColor, 
            IList<PlayerAction> preRollActions = null,
            RollDice rollAction = null,
            IList<PlayerAction> postRollActions = null
        ) 
        {
            Id = id;
            PlayerColor = playerColor;
            _preRollActions = new Stack<PlayerAction>(preRollActions?.ToList() ?? new List<PlayerAction>());
            _rollAction = rollAction;
            _postRollActions = new Stack<PlayerAction>(postRollActions?.ToList() ?? new List<PlayerAction>());
        }

        public void PlayDevelopmentCard(UseDevelopmentCard playerAction)
        {
            // Ensure we are allowed to play this card
            var cardType = playerAction.DevelopmentCardType;
            var cardsPlayed = _preRollActions
                .Where(x => x.GetType().IsAssignableFrom(typeof(UseDevelopmentCard)))
                .Cast<UseDevelopmentCard>()
                .Where(x => x.DevelopmentCardType.Equals(cardType) && x.PlayerColor.Equals(playerAction.PlayerColor))
                .Count();
            if (cardsPlayed > cardType.MaxPerTurn)
                throw new ActionLimitReachedException(playerAction);
            if (_rollAction != null) 
                _postRollActions.Push(playerAction);
            else
                _preRollActions.Push(playerAction);
        }

        public void RollDice(RollDice roll) 
        {
            if (_rollAction != null)
                throw new ActionLimitReachedException(roll);
            if (_postRollActions.Any()) 
                throw new InvalidOperationException("Cannot roll dice after post-roll actions have been recorded");
            _rollAction = roll;
        }

        public void AddAction(PlayerAction playerAction)
        {
            if (playerAction.GetType().IsAssignableFrom(typeof(UseDevelopmentCard)))
            {
                PlayDevelopmentCard(playerAction as UseDevelopmentCard);
                return;
            }
            if (playerAction.GetType().IsAssignableFrom(typeof(RollDice)))
            {
                RollDice(playerAction as RollDice);
                return;
            }
            if (_rollAction == null) 
                _preRollActions.Push(playerAction);
            else
                _postRollActions.Push(playerAction);
        }

        public void UndoAction()
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

    public class ActionLimitReachedException : InvalidOperationException 
    {
        public ActionLimitReachedException(PlayerAction playerAction) 
            : base($"Action ({playerAction.Description}) cannot be performed again this turn") { }
    }
}