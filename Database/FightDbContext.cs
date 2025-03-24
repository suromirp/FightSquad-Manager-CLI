using FightSquad___CLI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightSquad___CLI.Database
{
    public class FightDbContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<Fighter> Fighters { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FightSquadDB;Trusted_Connection=True;");
        }
    }
}
