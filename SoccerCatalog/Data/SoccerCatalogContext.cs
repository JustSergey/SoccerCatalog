using Microsoft.EntityFrameworkCore;
using SoccerCatalog.Models;

namespace SoccerCatalog.Data
{
    public class SoccerCatalogContext : DbContext
    {
        public SoccerCatalogContext(DbContextOptions<SoccerCatalogContext> options)
            : base(options) { }

        public DbSet<Player> Players {get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
