using Backend.Presentation.DTOs;

namespace Backend.Presentation.Services
{
    public interface ITournamentService
    {
        Task<int> CreateTournamentAsync(CreateTournamentDto dto, CancellationToken cancellationToken = default);
        Task UpdateMatchResultAsync(UpdateMatchResultDto dto, CancellationToken ct);
        Task<TeamComparisonDto> CompareTeamsAsync(int teamAId, int teamBId,CancellationToken ct);
        Task<TournamentViewDto> GetPublicTournamentDataAsync(int id, CancellationToken ct);
        Task<int> PlayerCreatesTeamAsync(string teamName, int tournamentId, int playerId, CancellationToken ct);
    }
}
