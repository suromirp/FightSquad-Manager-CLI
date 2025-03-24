using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSquad___CLI.Models
{
    public class Team
    {
        // Properties
        // Dont change the name of these properties, they are used by EF Core
        public int TeamId { get; set; }
        public string Name { get; set; }

        // Team has multiple fighters
        public List<Fighter> Fighters { get; set; } = new List<Fighter>();

        // Constructor
        public Team()
        {
            Name = string.Empty; // Initialize Name to avoid CS8618 error
        }
        public Team(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        // Methods
        public void AddFighter(Fighter fighter)
        {
            if (fighter == null)
            {
                throw new ArgumentNullException(nameof(fighter));
            }
            Fighters.Add(fighter);
            Console.WriteLine($"{fighter.Name} = added to {Name}");
        }
    }
}
