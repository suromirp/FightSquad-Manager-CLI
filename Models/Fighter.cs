using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSquad___CLI.Models
{
    public class Fighter
    {
        // Properties
        // Don't change the name of these properties, they are used by EF Core
        public int FighterId { get; set; }
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public int TeamId { get; set; } // Foreign key
        public Team Team { get; set; } = null!; // Navigation property

        public Fighter()
        {
            Name = string.Empty; // Initialize Name to avoid CS8618 error
        }

        // Constructor
        public Fighter(string name, int wins, int losses, int teamId)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name), "Fighter name cannot be empty.");
            Wins = wins;
            Losses = losses;
            TeamId = teamId;
        }

        // Methods
        public override string ToString()
        {
            return $"{Name} ({Wins}-{Losses}) - W/L";
        }
    }
}
