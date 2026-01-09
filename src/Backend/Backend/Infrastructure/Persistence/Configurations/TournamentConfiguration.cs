using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Domain.Entities.Tournaments;

namespace Backend.Infrastructure.Persistence.Configurations
{
    public class TournamentConfiguration : IEntityTypeConfiguration<Tournament>
    {
        public void Configure(EntityTypeBuilder<Tournament> builder)
        {
            builder.HasKey(t => t.Id);
            
            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);
            builder.Property(t => t.Description)
                .HasMaxLength(1000);
            builder.Metadata
                .FindNavigation(nameof(Tournament.Teams))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(t => t.Teams)
                .WithOne()
                .HasForeignKey("TournamentId")
                .OnDelete(DeleteBehavior.Cascade);
            builder.Metadata
                .FindNavigation(nameof(Tournament.Matches))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            
            builder.HasMany(t => t.Matches)
                .WithOne()
                .HasForeignKey(m => m.TournamentId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
