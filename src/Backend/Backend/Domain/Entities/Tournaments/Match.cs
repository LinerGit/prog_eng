using Backend.Domain.Common;

namespace Backend.Domain.Entities.Tournaments
{
    public class Match : Entity
    {
        public int TournamentId { get; set; }
        public int TeamAId { get; set; }
        public Team TeamA { get; set; } = null!;
        public int TeamBId { get; set; }
        public Team TeamB { get; set; } = null!;
        public string Score { get; private set; } = "0:0";

        public void UpdateScore(int t1, int t2) => Score = $"{t1}:{t2}";
        public DateTime ScheduledAt { get; private set; }
        public Match(int tournamentId, int teamAId, int teamBId, DateTime scheduledAt)
        {
            if (teamAId == teamBId)
                throw new ArgumentException("Команда не может играть сама с собой");

            TournamentId = tournamentId;
            TeamAId = teamAId;
            TeamBId = teamBId;
            ScheduledAt = scheduledAt;
        }
        private Match() { }
    }
}
