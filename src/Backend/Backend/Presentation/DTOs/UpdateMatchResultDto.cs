namespace Backend.Presentation.DTOs
{
    public record PlayerMatchStats(int PlayerId, int Kills, int Deaths, int Assists);

    public record UpdateMatchResultDto(
        int MatchId,
        int TeamAScore,
        int TeamBScore,
        List<PlayerMatchStats> AllPlayersStats 
    );

} 
