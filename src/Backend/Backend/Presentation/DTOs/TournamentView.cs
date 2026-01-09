namespace Backend.Presentation.DTOs;
public record MatchResultDto(
    int Id,
    string TeamAName,
    string TeamBName,
    string Score,
    DateTime ScheduledAt
);

public record TournamentViewDto(
    string Name,
    string Description,
    DateTime StartDate,
    List<string> TeamNames,
    List<MatchResultDto> Matches
);