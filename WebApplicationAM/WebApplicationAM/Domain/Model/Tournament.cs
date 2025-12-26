namespace WebApplicationAM.Domain.Model
{
    public class Tournament
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        required public DateTime Date { get; set; }

        public string? Description { get; set; }
        public required int MaxTeams { get; set; }
        public int MinTeams { get; set; } = 0;
        public IEnumerable<Team> Teams { get; set; } = new List<Team>();
        public Match[]? Matches { get; set; }
    }
}
