using System;

namespace BrickPort.Domain.Models
{
    public class RollResult
    {
        public int Die1 { get; }
        public int Die2 { get; }
        
        public int Value => Die1 + Die2;
        public bool ForceDiscard => Value == 7;
        public bool ForceMoveRobber => Value == 7;
        
        public RollResult(int die1, int die2)
        {
            Die1 = die1;
            Die2 = die2;
        }
    }
}
