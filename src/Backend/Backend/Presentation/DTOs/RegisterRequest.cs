namespace Backend.Presentation.DTOs
{
    public record RegisterRequest(string Username, string Email, string Password, string? RoleName);
}
