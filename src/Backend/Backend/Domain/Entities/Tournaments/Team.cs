using Backend.Domain.Common;

namespace Backend.Domain.Entities.Tournaments
{
    public class Team : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Player> Players { get; set; } = new List<Player>();

    }
}
