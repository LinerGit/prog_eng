namespace WebApplicationAM.Domain.Model
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public required bool IsCaptain { get; set; }
        public Team Team { get; set; }

    }
}
