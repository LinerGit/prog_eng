using Backend.Domain.Entities.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Backend.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            // Создаем уникальный индекс для Username
            builder.HasIndex(u => u.Username).IsUnique();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(150);

            // Создаем уникальный индекс для Email
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            // Конфигурация связи "Один роль ко многим пользователям"
            builder.HasOne(u => u.Role)
                .WithMany() // У роли нет списка пользователей в классе Role (List<User>)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); // Запретить удаление роли, если есть пользователи
        }
    
    }
}
