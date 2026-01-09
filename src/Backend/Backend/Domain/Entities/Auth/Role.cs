namespace Backend.Domain.Entities.Auth
{
    public class Role : IEquatable<Role>
    {
        public string Name { get; set; } = string.Empty;
        public bool Equals(Role? other)
        {
            if (other is null) return false;
            return Name.Equals(other.Name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
