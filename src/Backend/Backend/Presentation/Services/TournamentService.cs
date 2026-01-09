using Backend.Domain.Entities.Tournaments;
using Backend.Domain.Interfaces;
using Backend.Domain.Services;
using Backend.Presentation.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Backend.Presentation.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITournamentRepository _repository;
        public TournamentService(IUnitOfWork unitOfWork, ITournamentRepository repository)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<int> CreateTournamentAsync(CreateTournamentDto dto, CancellationToken cancellationToken = default)
        {
            var tournament = new Tournament
            (
                dto.Name,
                dto.StartDate,
                dto.EndDate,
                dto.Description
            );
            _repository.Add(tournament);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return tournament.Id;
        }
        public async Task UpdateMatchResultAsync(UpdateMatchResultDto dto, CancellationToken ct)
        {
            
            var match = await _repository.GetMatchWithPlayersAsync(dto.MatchId, ct);
            if (match == null) throw new KeyNotFoundException("Матч не найден");

            match.UpdateScore(dto.TeamAScore, dto.TeamBScore);

            var playersInMatch = match.TeamA.Players
                .Concat(match.TeamB.Players)
                .ToList();

            foreach (var statsDto in dto.AllPlayersStats)
            {
                var player = playersInMatch.FirstOrDefault(p => p.Id == statsDto.PlayerId);
                if (player == null)
                {
                    throw new Exception($"Игрок с ID {statsDto.PlayerId} не зарегистрирован в этом матче!");
                }
                player?.UpdatePerformance(statsDto.Kills, statsDto.Deaths, statsDto.Assists);
            }
            await _unitOfWork.SaveChangesAsync(ct);
        }
        public async Task<TeamComparisonDto> CompareTeamsAsync(int teamAId, int teamBId, CancellationToken ct)
        {
            
            var teams = await _repository.GetTeamsWithPlayersAsync(teamAId, teamBId, ct) ?? throw new KeyNotFoundException($"one pr two of teams with ids: {teamAId},{teamBId}  not found");

            if (teams.Count < 2)
                throw new KeyNotFoundException("Одна или обе команды не найдены");

            var teamA = teams.First(t => t.Id == teamAId);
            var teamB = teams.First(t => t.Id == teamBId);

            var balanceService = new BalanceService();
            var report = balanceService.EvaluateBalance(teamA, teamB);

            return new TeamComparisonDto(
                teamA.Name,
                teamB.Name,
                report.TeamAPower,
                report.TeamBPower,
                report.Difference
            );
        }
        public async Task<TournamentViewDto> GetPublicTournamentDataAsync(int id, CancellationToken ct)
        {
            var tournament = await _repository.GetTournamentViewAsync(id, ct) ?? throw new KeyNotFoundException($"tournament with id: {id} not found"); 

            var teamNames = tournament.Teams.Select(t => t.Name).ToList();

            var matches = tournament.Matches.Select(m => new MatchResultDto(
                m.Id,
                m.TeamA.Name,
                m.TeamB.Name,
                m.Score,
                m.ScheduledAt
            )).OrderBy(m => m.ScheduledAt).ToList();

            return new TournamentViewDto(
                tournament.Name,
                tournament.Description,
                tournament.StartDate,
                teamNames,
                matches
            );
        }
        public async Task<int> PlayerCreatesTeamAsync(string teamName, int tournamentId, int playerId, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(teamName))
                throw new ArgumentException("Название команды не может быть пустым");

            var playerExists = await _repository.PlayerExistsAsync(playerId, ct);
            if (!playerExists)
                throw new KeyNotFoundException($"Игрок с ID {playerId} не найден");

            return await _repository.CreateTeamRawSqlAsync(teamName, tournamentId, playerId, ct);
        }
    }
}
