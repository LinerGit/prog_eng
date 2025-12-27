using Backend.Domain.Common;

namespace Backend.Domain.Entities.Tournaments
{
    public class Player : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Nickname { get; set; } = string.Empty;
        public int Kills { get; private set; }
        public int Deaths { get; private set; }
        public int Assists { get; private set; }
        public int TeamId { get; set; }
        public Team? Team { get; set; } = null;

        public void UpdatePerformance(int kills, int deaths, int assists)
        {
            Kills = kills;
            Deaths = deaths;
            Assists = assists;
        }
    }
}
