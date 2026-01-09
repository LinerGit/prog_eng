using System.ComponentModel.DataAnnotations;

namespace Backend.Presentation.DTOs
{
    public record CreateTournamentDto
    (
        [Required] string Name,

        [Required] DateTime StartDate,

        [Required] DateTime EndDate,
        
        string? Description
    );
}
