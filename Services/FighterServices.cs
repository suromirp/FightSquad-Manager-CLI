using FightSquad___CLI.Helper;
using FightSquad___CLI.Models;
using FightSquad___CLI.UI;
using FightSquad___CLI.Database;

namespace FightSquad___CLI.Services
{
    class FighterServices
    {
        // Link to the database context
        private static readonly FightDbContext _context = new();

        public static void AddFighter(Team? team = null)
        {
            // If no team is provided, let the user select one
            if (team == null)
            {
                if (TeamServices.Teams.Count == 0)
                {
                    Console.WriteLine("There are no Teams to add Fighters to!");
                    Console.ReadKey();
                    return;
                }

                int selectedIndex = ArrowSelector.TeamselectionMenu(TeamServices.Teams.ConvertAll(t => t.Name));
                if (selectedIndex == -1) return; // Escape button
                team = TeamServices.Teams[selectedIndex]; // Set the selected team
            }

            // Ensure the team is saved to the database
            if (!_context.Teams.Any(t => t.TeamId == team.TeamId))
            {
                _context.Teams.Add(team);
                _context.SaveChanges();
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Adding Fighter to team: {team.Name}");

                // Use the ValidateNameAndPrompt method to get a valid name for the Fighter
                ValidationHelper.ValidateNameAndPrompt("Enter the Fighter's name: ", out string name);

                // If the name is empty, return
                if (string.IsNullOrWhiteSpace(name)) return;

                int wins;
                while (true)
                {
                    Console.WriteLine($"Enter the number of wins Fighter {name} has");
                    string? input = Console.ReadLine();

                    // Ensure the input is a number between 0 and 300
                    if (int.TryParse(input, out wins) && wins > -1 && wins < 300)
                    {
                        break;
                    }
                    else if (wins < -1)
                    {
                        Console.WriteLine("Please enter a positive number");
                    }
                    else if (wins > 300)
                    {
                        Console.WriteLine("Lets be fair, nobody got that many!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!, please enter a number");
                    }
                }

                int losses;
                while (true)
                {
                    Console.WriteLine($"Enter the number of losses Fighter {name} has");
                    string? input = Console.ReadLine();

                    if (int.TryParse(input, out losses) && losses > -1 && losses < 300)
                    {
                        break;
                    }
                    else if (losses < -1)
                    {
                        Console.WriteLine("Invalid input! Please enter a positive number");
                    }
                    else if (losses > 300)
                    {
                        Console.WriteLine("Lets be fair, nobody got that many!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!, please enter a number");
                    }
                }

                // If the name is not empty, create a new Fighter and add it to the team
                if (!string.IsNullOrWhiteSpace(name) && ValidationHelper.IsValidName(name))
                {
                    Fighter newFighter = new(name, wins, losses, team.TeamId);
                    team.Fighters.Add(newFighter);
                    _context.Fighters.Add(newFighter);
                    _context.SaveChanges();
                    Console.WriteLine($"\nFighter {name} added to Team {team.Name} with the W/L record of {wins}-{losses}");
                }
                else
                {
                    Console.WriteLine("\nOops, something went wrong. Fighter was not added");
                }

                // Ask the user if they want to add another fighter
                Console.WriteLine("\nDo you want to add another fighter? (y/n): ");
                string? addAnother = Console.ReadLine()?.Trim().ToLower();
                if (addAnother == "y")
                {
                    // Nothing, loop will keep going
                }
                else if (addAnother == "n")
                {
                    break; // Exit the loop, when user does not want to add another fighter
                }
                else
                {
                    Console.WriteLine("Invalid input, please type y or n (yes or no)");
                    Console.ReadKey();
                }
            }
        }

        public static void EditFighters(Team team)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Editing Fighters in Team {team.Name} \n");

                // If the team has no fighters, ask the user if they want to add one
                if (team.Fighters.Count == 0)
                {
                    Console.WriteLine("This Team seems to not have any Fighters\n");
                    Console.WriteLine("Would you like to add a Fighter? (Press Enter to add or Escape to go back)");

                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (key == ConsoleKey.Escape) return; // Escape button to go back

                    AddFighter(team);
                    continue; // Restart the loop to show the updated list of fighters
                }

                // Selection menu with existing fighters and additional Teams
                List<string> fighterTeams = team.Fighters.Select(f => $"{f.Name} ({f.Wins}-{f.Losses})").ToList();
                fighterTeams.Add("Add Fighter");
                fighterTeams.Add("Back");

                int selectedIndex = ArrowSelector.TeamselectionMenu(fighterTeams);
                if (selectedIndex == -1 || selectedIndex == fighterTeams.Count - 1) return;

                if (selectedIndex == fighterTeams.Count - 2)
                {
                    AddFighter(team);
                    continue; // Restart the loop to show the updated list of fighters
                }

                Fighter selectedFighter = team.Fighters[selectedIndex];

                Console.Clear();
                Console.WriteLine($"Editing Fighter: {selectedFighter.Name} ({selectedFighter.Wins}-{selectedFighter.Losses})\n");

                // Prompt the user to enter new values for the Fighter or press Enter to keep the current values
                Console.Write($"Enter a new name for {selectedFighter.Name} (or press Enter to keep current name): ");
                string? newName = Console.ReadLine()?.Trim();
                ValidationHelper.ValidateNameAndPrompt("Enter a valid Fighter name: ", out newName);
                selectedFighter.Name = newName;
                Console.WriteLine("Fighter's name successfully updated!");

                // Prompt the user to enter new values for the Fighter or press Enter to keep the current values
                Console.Write($"Enter new number of Wins for {selectedFighter.Name} (or press Enter to keep current value): ");
                string? winsInput = Console.ReadLine();
                if (int.TryParse(winsInput, out int newWins))
                {
                    selectedFighter.Wins = newWins;
                    Console.WriteLine("Fighter's wins successfully updated!");
                }

                // Prompt the user to enter new values for the Fighter or press Enter to keep the current values
                Console.Write($"Enter new number of Losses for {selectedFighter.Name} (or press Enter to keep current value): ");
                string? lossesInput = Console.ReadLine();
                if (int.TryParse(lossesInput, out int newLosses))
                {
                    selectedFighter.Losses = newLosses;
                    Console.WriteLine("Fighter's losses successfully updated!");
                }

                _context.SaveChanges(); // Save changes to the database

                Console.WriteLine("\nFighter updated successfully!");
                Console.ReadKey();
            }
        }

        public static void RemoveFighter(Team team)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Remove Fighter - {team.Name}\n");

                // If the team has no fighters, show a message and return
                if (team.Fighters.Count == 0)
                {
                    Console.WriteLine("This team has no fighters to delete");
                    Console.ReadKey();
                    return;
                }

                // List with Fighters for selection and an option to go back
                List<string> fighterTeams = team.Fighters.Select(f => $"ID:{f.Name} ({f.Wins}-{f.Losses})").ToList();
                fighterTeams.Add("Back");

                int selectedIndex = ArrowSelector.TeamselectionMenu(fighterTeams);

                // If the user presses Escape or "Back", return
                if (selectedIndex == -1 || selectedIndex == fighterTeams.Count - 1)
                {
                    return; // Escape button or "back" brings user back to main menu
                }

                Fighter selectedFighter = team.Fighters[selectedIndex];

                // Ask the user for confirmation before deleting the Fighter
                Console.WriteLine($"\nAre you sure you want to remove {selectedFighter.Name}? (y/n)");
                string? confirmation = Console.ReadLine()?.Trim().ToLower();

                if (confirmation == "y")
                {
                    team.Fighters.Remove(selectedFighter);
                    _context.Fighters.Remove(selectedFighter);
                    _context.SaveChanges();
                    Console.WriteLine($"Fighter {selectedFighter.Name} has been removed");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Action cancelled");
                    Console.ReadKey();
                }
            }
        }
    }
}
