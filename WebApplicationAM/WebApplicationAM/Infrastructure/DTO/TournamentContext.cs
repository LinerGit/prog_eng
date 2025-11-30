using Microsoft.EntityFrameworkCore;
using WebApplicationAM.Domain.Model;

namespace WebApplicationAM.Infrastructure.DTO
{
    public partial class TournamentContext: DbContext
       
    {
        private IConfiguration _configuration;

        public TournamentContext(DbContextOptions<TournamentContext> options, IConfiguration configuration)
            : base(options)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Organisator> Organisators => Set<Organisator>();
        public DbSet<Team> Teams => Set<Team>();  
        public DbSet<Tournament> Tournaments => Set<Tournament>();   
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("Server");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.IsCaptain).IsRequired();
            });
            modelBuilder.Entity<Organisator>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.Email).IsRequired();
            });
            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });
            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.MaxTeams).IsRequired();
            });

        }
    }
}
