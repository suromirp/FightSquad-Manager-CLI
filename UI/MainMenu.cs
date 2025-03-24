using FightSquad___CLI.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSquad___CLI.UI
{
    class MainMenu()
    {
        private bool running = true;

        public void Show()
        {
            while (running)
            {
                Console.Clear();
                Console.WriteLine("Fight Squad UFC Manager");

                Console.WriteLine("1. Create your Team");
                Console.WriteLine("2. Add fighters to Team");
                Console.WriteLine("3. Edit Team & Fighters");
                Console.WriteLine("4. Delete Team & Fighters");
                Console.WriteLine("5. View list of Teams & Fighters");
                Console.WriteLine("6. Close the application");

                Console.WriteLine("\nEnter the number of the menu you wish to open");
                string? choice = Console.ReadLine();
                if (choice != null)
                {
                    MenuChoice(choice);
                }
                else
                {
                    Console.WriteLine("Type a valid number to pick your action");
                }
            }    
        }

        private void MenuChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    TeamServices.CreateTeam();
                    break;
                case "2":
                    FighterServices.AddFighter();
                    break;
                case "3":
                    TeamServices.EditTeam();
                    break;
                case "4":
                    TeamServices.DeleteTeam();
                    break;
                case "5":
                    TeamServices.TeamOverview();
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Application stopped");
                    break;
                default:
                    Console.WriteLine("Invalid choice, please select a valid number");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
