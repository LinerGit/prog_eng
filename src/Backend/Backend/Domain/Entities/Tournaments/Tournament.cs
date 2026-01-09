using Backend.Domain.Common;


namespace Backend.Domain.Entities.Tournaments
{
    public class Tournament : Entity, IAggregateRoot
    {
        
        public string Name { get; private set; } 
        public DateTime StartDate { get; private set; }     
        public DateTime EndDate { get; private set; }
        public string Description { get; private set; } 
        private readonly List<Team> _teams = new();
        public IReadOnlyCollection<Team> Teams => _teams.AsReadOnly();
        private readonly List<Match> _matches = new();
        public IReadOnlyCollection<Match> Matches => _matches.AsReadOnly();

        public Tournament(string name, DateTime startDate, DateTime endDate, string description)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required");
            if (endDate < startDate) throw new ArgumentException("End date cannot be before start date");

            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description ?? string.Empty;
        }
        public void RegisterTeam(Team team)
        {
            // Проверка правила: нельзя регистрировать команды, если турнир начался
            if (DateTime.UtcNow > StartDate)
                throw new InvalidOperationException("Cannot register team after tournament started");

            if (_teams.Any(t => t.Id == team.Id))
                throw new InvalidOperationException("Team already registered");

            _teams.Add(team);
        }
        public void ScheduleMatch(int teamAId, int teamBId, DateTime scheduledAt)
        {
            if (!_teams.Any(t => t.Id == teamAId) || !_teams.Any(t => t.Id == teamBId))
                throw new InvalidOperationException("Обе команды должны быть участниками турнира");

            var match = new Match(Id, teamAId, teamBId, scheduledAt);
            _matches.Add(match);
        }

    }
}
