using System;

namespace BrickPort.Domain.Models
{
    
    public interface IPlayerAction 
    { 
        int PointValue { get; }
        PlayerColor Player { get; }
        string Description { get; }
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
        PlayerColor FromPlayer { get; }
    }

    public abstract class PlayerAction : IPlayerAction
    {
        public Guid Id { get; }
        public PlayerColor Player { get; }
        public int PointValue { get; }
        public virtual string Description => this.GetType().Name;

        public PlayerAction(Guid id, PlayerColor player, int pointValue = 0) 
        {
            Id = id;
            Player = player;
            PointValue = pointValue;
        }
    }

    public abstract class TriggeredPlayerAction : PlayerAction, ITriggeredAction
    {
        public IPlayerAction TriggeredBy { get; }
        public TriggeredPlayerAction(Guid id, PlayerColor player, IPlayerAction triggeredBy, int victoryPoints = 0) 
            : base(id, player, victoryPoints) => TriggeredBy = triggeredBy;
    }

    public abstract class NewLeaderAction : TriggeredPlayerAction, INewLeaderAction 
    {
        public PlayerColor FromPlayer { get; }
        public int? OldMax { get; }
        public int? NewMax { get; }
        
        public NewLeaderAction(
            Guid id, 
            PlayerColor player, 
            int victoryPoints, 
            IPlayerAction triggeredBy,
            int? newMax,
            PlayerColor fromPlayer = null, 
            int? oldMax = null
        ) : base(id, player, triggeredBy, victoryPoints)
        {
            if (!newMax.HasValue && !oldMax.HasValue)
                throw new InvalidOperationException("Lead changes require old max or new max value(s) to be specified");
            if ((newMax.HasValue && Player == null) || (Player != null && !newMax.HasValue))
                throw new InvalidOperationException("New player and new max value(s) must both be provided");
            if ((oldMax.HasValue && fromPlayer == null) || (FromPlayer != null && !oldMax.HasValue))
                throw new InvalidOperationException("From player and old max value(s) must both be provided");
            FromPlayer = fromPlayer;
            OldMax = oldMax;
            NewMax = newMax;
        }
    }

}