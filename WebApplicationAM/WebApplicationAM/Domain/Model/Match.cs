namespace WebApplicationAM.Domain.Model
{
    public class Match
    {
        public int Id { get; set; }
        public required Team TeamA { get; set; }
        public required Team TeamB { get; set; }
        public required int ScoreA { get; set; }
        public required int ScoreB { get; set; }
        public required DateTime MatchDate { get; set; }

    }
}
