using FightSquad___CLI.Database;
using FightSquad___CLI.Helper;
using FightSquad___CLI.Models;
using FightSquad___CLI.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSquad___CLI.Services
{
        class TeamServices
        {

            private static readonly FightDbContext _context = new FightDbContext();
            public static List<Team> Teams = new List<Team>();

            static TeamServices()
            {
                Teams = _context.Teams.Include(t => t.Fighters).ToList();
            }

        public static void CreateTeam()
        {
            Console.Clear();
            Console.WriteLine("Press Escape (Esc) to go back at any time\n");
            Console.WriteLine("Press Enter (entr) to confirm your Team name\n");

            ValidationHelper.ValidateNameAndPrompt("Enter the name of your Team: ", out string teamName);

            // If escape is pressed, return
            if (string.IsNullOrWhiteSpace(teamName)) return;

            Team newTeam = new Team(teamName);
            Teams.Add(new Team(teamName));
            _context.Teams.Add(newTeam); // Add the new team to _context
            _context.SaveChanges(); // Save changes to the database
            Console.WriteLine($"\nTeam '{teamName}' successfully created!");
            Console.ReadKey();
        }

        public static void EditTeam()
            {
            Console.Clear();

            // If there are no teams, show a message and return
            if (Teams.Count == 0)
            {
                Console.WriteLine("There are no Teams to edit!");
                Console.ReadKey();
                return;
            }

            // Select a team to edit with ArrowSelector
            int selectedIndex = ArrowSelector.TeamselectionMenu(Teams.ConvertAll(t => $"ID:{t.Name}"));

            // If the user presses Escape, return
            if (selectedIndex == -1) return;

            Team selectedTeam = Teams[selectedIndex];

            // while loop to keep the user in the edit menu until they press "Back"
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Editing Team: {selectedTeam.Name} \n");

                // List of Teams for the user to choose from
                List<string> Teams = new List<string> { "Change Team Name", "Edit Fighters", "Back" };
                int optionIndex = ArrowSelector.TeamselectionMenu(Teams);
                if (optionIndex == -1) return;

                switch (optionIndex)
                {
                    case 0:
                       // Use ValidateNameAndPrompt to ensure a valid team name
                       ValidationHelper.ValidateNameAndPrompt($"Enter a new name for team {selectedTeam.Name}: ", out string newName);

                       // If Esc was pressed, no changes are made
                       if (string.IsNullOrWhiteSpace(newName)) return;

                       selectedTeam.Name = newName;
                        _context.Teams.Update(selectedTeam); // Update the team in the _context
                        _context.SaveChanges(); // Save changes to the database
                        Console.WriteLine($"Team name successfully changed to {selectedTeam.Name}");
                       Console.ReadKey();
                       break;
                    case 1:
                        FighterServices.EditFighters(selectedTeam);
                        break;
                    case 2:
                        return;
                }
            }
        }

        public static void DeleteTeam()
        {
            while (true)
            {
                Console.Clear();

                if (Teams.Count == 0)
                {
                    Console.WriteLine("There are no Teams to delete!");
                    Console.ReadKey();
                    return;
                }

                // Use ArrowSelector to select a team to delete
                int selectedIndex = ArrowSelector.TeamselectionMenu(Teams.ConvertAll(t => $"{t.Name}"));
                if (selectedIndex == -1) return; // Escape button to go back

                Team selectedTeam = Teams[selectedIndex]; // Set the selected team

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Delete Menu - {selectedTeam.Name}\n");
                    Console.WriteLine("Please make a choice:");

                    // Give the user Teams to delete Fighters, the Team or go back
                    List<string> deleteTeams = new List<string> { "Delete Fighters", "Delete Team", "Back" };
                    int optionIndex = ArrowSelector.TeamselectionMenu(deleteTeams);
                    if (optionIndex == -1 || optionIndex == 2) break;

                    switch (optionIndex)
                    {
                        case 0: // Let RemoveFighter handle the deletion
                            FighterServices.RemoveFighter(selectedTeam);
                            break;

                        case 1:
                            Console.WriteLine($"\nAre you sure you want to delete {selectedTeam.Name}? (y/n): ");
                            string? confirmTeamDelete = Console.ReadLine()?.Trim().ToLower();

                            if (confirmTeamDelete == "y")
                            {
                                Teams.Remove(selectedTeam);
                                _context.Teams.Remove(selectedTeam); // Remove the team from the _context
                                _context.SaveChanges(); // Save changes to the database

                                Console.WriteLine($"Team {selectedTeam.Name} has been deleted");
                                Console.ReadKey();
                                return; // Exit after deleting the team, to avoid editing a deleted team
                            }
                            else if (confirmTeamDelete == "n")
                            {
                                Console.WriteLine($"{selectedTeam.Name} was not deleted");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.WriteLine($"Please type y or n, to make a choice");
                                Console.ReadKey();
                            }
                            break;
                    }
                }
            }
        }

        public static void TeamOverview()
        {
            Console.Clear();

            // If there are no teams, show a message and return
            if (Teams.Count == 0)
            {
                Console.WriteLine("There are no Teams to delete!");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                // Use ArrowSelector to select a team to view
                int selectedIndex = ArrowSelector.TeamselectionMenu(Teams.ConvertAll(t => t.Name));
                if (selectedIndex == -1) return;

                Team selectedTeam = Teams[selectedIndex];

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine($"Team: {selectedTeam.Name}\n");

                    // Display the Fighters in the selected team or show a message if there are none
                    if (selectedTeam.Fighters.Count == 0)
                    {
                        Console.WriteLine("This team has no Fighters.\nPress Escape (esc) to go back.");
                    }
                    else
                    {
                        Console.WriteLine("Fighters:\n");
                        foreach (var fighter in selectedTeam.Fighters)
                        {
                            Console.WriteLine($"- {fighter}");
                        }
                        Console.WriteLine("\nPress Escape (esc) to go back");
                    }
                    
                    // Allow user to go back with the Escape key
                    if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        break;
                }
            }
        }
    }
}
