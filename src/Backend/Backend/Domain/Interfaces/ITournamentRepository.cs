using Backend.Domain.Common;
using Backend.Domain.Entities.Tournaments;
using Microsoft.EntityFrameworkCore;

namespace Backend.Domain.Interfaces
{
    public interface ITournamentRepository : IRepository<Tournament>
    {
        Task<Tournament?> GetWithTeamsAsync(int id);
        Task<Match?> GetMatchWithPlayersAsync(int matchId, CancellationToken ct);
        Task<List<Team>> GetTeamsWithPlayersAsync(int teamAId, int teamBId, CancellationToken ct);
        Task<Tournament?> GetTournamentViewAsync(int id, CancellationToken ct);
        Task<int> CreateTeamRawSqlAsync(string teamName, int tournamentId, int creatorPlayerId, CancellationToken ct);
        Task<bool> PlayerExistsAsync(int playerId, CancellationToken ct);

    }
}
