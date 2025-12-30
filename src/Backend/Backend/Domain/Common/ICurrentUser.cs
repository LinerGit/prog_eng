namespace Backend.Domain.Common
{
    public interface ICurrentUser
    {
        int UserId { get; }
        string Email { get; }
        string Role { get; }

    }
}
