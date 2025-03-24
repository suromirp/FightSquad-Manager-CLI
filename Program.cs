using FightSquad___CLI.Database;
using FightSquad___CLI.UI;

namespace FightSquad___CLI
{
    class Program
    {
        static void Main()
        {
            // Seed the database with predefined teams and fighters
            SeedDatabase.Seed();

            MainMenu menu = new MainMenu();
            menu.Show();
        }
    }
}