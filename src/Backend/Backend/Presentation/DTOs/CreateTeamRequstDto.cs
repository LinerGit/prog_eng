using System.ComponentModel.DataAnnotations;

namespace Backend.Presentation.DTOs;

public record CreateTeamRequestDto(
    [Required][StringLength(100)] string TeamName,
    [Range(1, int.MaxValue)] int TournamentId,
    [Range(1, int.MaxValue)] int PlayerId
);