using System;

namespace BrickPort.Domain.Models
{
    
    public interface IPlayerAction 
    { 
        int PointValue { get; }
        PlayerColor PlayerColor { get; }
        string Description { get; }
        GameState Apply(GameState gameState);
    }

    public interface ISpecialBuildPhaseEligibleAction : IPlayerAction { }

    public interface IBuildAction : ISpecialBuildPhaseEligibleAction { }

    public interface IPreGameAction : IPlayerAction { }

    public interface IPreRollAction : IPlayerAction { }

    public interface IRollAction : IPlayerAction 
    {
        RollResult RollResult { get; }
    }

    public interface IUseDevelopmentCardAction : IPreRollAction
    {
        DevelopmentCardType CardType { get; }
    }

    public interface ITriggeredAction : ISpecialBuildPhaseEligibleAction 
    { 
        IPlayerAction TriggeredBy { get; }
    }

    public interface INewLeaderAction : ITriggeredAction
    { 
        PlayerColor FromPlayerColor { get; }
    }

    public abstract class PlayerAction : IPlayerAction
    {
        public Guid Id { get; }
        public PlayerColor PlayerColor { get; }
        public int PointValue { get; }
        public virtual string Description => this.GetType().Name;

        public PlayerAction(Guid id, PlayerColor playerColor, int pointValue = 0) 
        {
            Id = id;
            PlayerColor = playerColor;
            PointValue = pointValue;
        }

        public abstract GameState Apply(GameState gameState);
    }

    public abstract class TriggeredPlayerAction : PlayerAction, ITriggeredAction
    {
        public IPlayerAction TriggeredBy { get; }
        public TriggeredPlayerAction(Guid id, PlayerColor player, IPlayerAction triggeredBy, int victoryPoints = 0) 
            : base(id, player, victoryPoints) => TriggeredBy = triggeredBy;
    }

    public abstract class NewLeaderAction : TriggeredPlayerAction, INewLeaderAction 
    {
        public PlayerColor FromPlayerColor { get; }
        public int? OldMax { get; }
        public int? NewMax { get; }
        
        public NewLeaderAction(
            Guid id, 
            PlayerColor playerColor, 
            int victoryPoints, 
            IPlayerAction triggeredBy,
            int? newMax,
            PlayerColor fromPlayerColor = null, 
            int? oldMax = null
        ) : base(id, playerColor, triggeredBy, victoryPoints)
        {
            if (!newMax.HasValue && !oldMax.HasValue)
                throw new InvalidOperationException("Lead changes require old max or new max value(s) to be specified");
            if ((newMax.HasValue && PlayerColor == null) || (PlayerColor != null && !newMax.HasValue))
                throw new InvalidOperationException("New player and new max value(s) must both be provided");
            if ((oldMax.HasValue && fromPlayerColor == null) || (FromPlayerColor != null && !oldMax.HasValue))
                throw new InvalidOperationException("From player and old max value(s) must both be provided");
            FromPlayerColor = fromPlayerColor;
            OldMax = oldMax;
            NewMax = newMax;
        }
    }

}