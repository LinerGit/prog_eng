namespace Backend.Domain.Entities.Tournaments
{
    public class Organisator
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Tournament> Tournaments { get; set; } = new List<Tournament>();
    }
}
