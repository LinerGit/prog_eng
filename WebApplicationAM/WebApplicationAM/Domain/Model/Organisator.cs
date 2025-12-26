namespace WebApplicationAM.Domain.Model
{
    public class Organisator
    {   public int Id { get; set; }
        public required string Name { get; set; }
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public int[]? TournamentIds { get; set; }
    }
}
