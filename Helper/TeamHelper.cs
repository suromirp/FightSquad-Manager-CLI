using FightSquad___CLI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSquad___CLI.Helper
{
        // Helper method to create predefined teams
        public static class TeamHelper
        {
            public static Team CreatePredefinedTeam(string name, List<Fighter> fighters)
            {
                Team team = new Team(name);
                foreach (var fighter in fighters)
                {
                    team.AddFighter(fighter);
                }
                return team;
            }
        }
}
