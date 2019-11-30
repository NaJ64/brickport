using System;

namespace BrickPort.Domain.Models
{
    public class DevelopmentCard
    {
        public DevelopmentCardType CardType { get; }
        public DevelopmentCard(DevelopmentCardType cardType) => CardType = cardType;
        public Player UsedBy { get; private set; }
        public int Points => CardType.PointValue;
        public void Use(Player player)
        {
            if (UsedBy != null)
                throw new CardAlreadyUsedException(this);
            UsedBy = player;
        }
    }

    public class CardAlreadyUsedException : InvalidOperationException
    {
        public CardAlreadyUsedException(DevelopmentCard card) : base($"Card has already been used ({card.CardType.Name})") { }
    }
}