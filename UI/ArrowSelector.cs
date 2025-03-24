using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSquad___CLI.UI
{
    class ArrowSelector
    {
        public static int TeamselectionMenu(List<string> Teams)
        {
            int selectedIndex = 0;
            ConsoleKey key = ConsoleKey.NoName;

            do
            {
                Console.Clear();
                Console.WriteLine("Use the arrow keys to navigate the list and press Enter (Ent) to confirm your selection");
                Console.WriteLine("Press the Escape (Esc) key to exit \n");

                // Display the Teams
                for (int i = 0; i < Teams.Count; i++)
                {
                    if (i == selectedIndex)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue; // Highlight the selected option
                        Console.WriteLine($"> {Teams[i]} <");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine(Teams[i]);
                    }
                }

                key = Console.ReadKey(true).Key;

                // Navigate up through Teams, looping back to the bottom when at the top or bottom    
                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = selectedIndex == 0 ? Teams.Count - 1 : selectedIndex - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = selectedIndex == Teams.Count - 1 ? 0 : selectedIndex + 1;
                }

                // Return if escape key is pressed
                else if (key == ConsoleKey.Escape)
                {
                    return -1;
                }

                // Return the selected index when the Enter key is pressed
            } while (key != ConsoleKey.Enter);

                return selectedIndex;
        }
    }
}
