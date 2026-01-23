using Backend.Domain.Entities.Auth;
using Backend.Domain.Entities.Tournaments;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Tournament> Tournaments => Set<Tournament>();
        public DbSet<Organisator> Organisators => Set<Organisator>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
