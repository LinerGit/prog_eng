using Backend.Domain.Entities.Tournaments;
using Backend.Domain.Interfaces;
using Backend.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly AppDbContext _context;

        public TournamentRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Tournament?> GetWithTeamsAsync(int id)
        {
            return await _context.Tournaments
                .Include(t => t.Teams)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IReadOnlyList<Tournament>> ListAllAsync()
        {
            return await _context.Tournaments
                .AsNoTracking()
                .ToListAsync();
            
        }

        public async Task<Tournament?> GetByIdAsync(int id)
        {
            return await _context.Tournaments.FindAsync(id);
        }

        public void Add(Tournament entity)
        {
             _context.Tournaments.Add(entity);
        }

        public void Update(Tournament entity)
        {
            _context.Tournaments.Update(entity);
            
        }

        public void Delete(Tournament entity)
        {
            _context.Tournaments.Remove(entity);
            
        }
        public async Task<Match?> GetMatchWithPlayersAsync(int matchId, CancellationToken ct)
        {
            return await _context.Matches
                .Include(m => m.TeamA).ThenInclude(t => t.Players)
                .Include(m => m.TeamB).ThenInclude(t => t.Players)
                .FirstOrDefaultAsync(m => m.Id == matchId, ct);
        }
        public async Task<List<Team>> GetTeamsWithPlayersAsync(int teamAId, int teamBId, CancellationToken ct)
        {
            return await _context.Teams
                .Include(t => t.Players)
                .Where(t => t.Id == teamAId || t.Id == teamBId)
                .ToListAsync(ct);
        }
        public async Task<Tournament?> GetTournamentViewAsync(int id, CancellationToken ct)
        {
            return await _context.Tournaments
                .Include(t => t.Teams)
                .Include(t => t.Matches)
                    .ThenInclude(m => m.TeamA)
                .Include(t => t.Matches)
                    .ThenInclude(m => m.TeamB)
                .FirstOrDefaultAsync(t => t.Id == id, ct);
        }
        public async Task<int> CreateTeamRawSqlAsync(string teamName, int tournamentId, int creatorPlayerId, CancellationToken ct)
        {
            var result = await _context.Database
                .SqlQueryRaw<int>(@"INSERT INTO ""Teams"" (""Name"", ""TournamentId"") 
                           VALUES (@name, @tId) RETURNING ""Id""",
                                   new Npgsql.NpgsqlParameter("@name", teamName),
                                   new Npgsql.NpgsqlParameter("@tId", tournamentId))
                .ToListAsync(ct);

            if (result == null || !result.Any())
            {
                throw new Exception("Не удалось создать команду. Проверьте ID турнира.");
            }

            var newTeamId = result.First();

            var rowsAffected = await _context.Database.ExecuteSqlInterpolatedAsync(
                $@"UPDATE ""Players"" SET ""TeamId"" = {newTeamId} WHERE ""Id"" = {creatorPlayerId}", ct);

            if (rowsAffected == 0)
            {
                throw new Exception("Команда создана, но не удалось привязать игрока.");
            }

            return newTeamId;
        }
        public async Task<bool> PlayerExistsAsync(int playerId, CancellationToken ct)
        {
            return await _context.Players.AnyAsync(p => p.Id == playerId, ct);
        }
    }
}
