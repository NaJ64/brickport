using System;

namespace BrickPort.Domain.Models
{
    public class Player
    {
        public Guid Id { get; }
        public string Name { get; }
        
        public Player(string name) : this(Guid.NewGuid(), name) { }
        public Player(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}