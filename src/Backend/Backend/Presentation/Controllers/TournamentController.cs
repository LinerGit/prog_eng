using Backend.Domain.Interfaces;
using Backend.Presentation.DTOs;
using Backend.Presentation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IAuthService _authService;
        public TournamentController(ITournamentService tournamentService, IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _tournamentService = tournamentService ?? throw new ArgumentNullException(nameof(tournamentService));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DTOs.RegisterRequest request)
        {
            try
            {
                // Вызываем сервис регистрации
                var token = await _authService.RegisterAsync(
                    request.Username,
                    request.Email,
                    request.Password,
                    request.RoleName ?? "User" // Роль по умолчанию, если не передана
                );

                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] DTOs.LoginRequest request)
        {
            try
            {
                var token = await _authService.LoginAsync(request.Email, request.Password);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // В реальных проектах лучше возвращать Unauthorized для логина
                return Unauthorized(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Organisator")]
        [HttpPost]
        public async Task<IActionResult> CreateTournament([FromBody] CreateTournamentDto dto, CancellationToken cancellationToken)
        {
            if (dto == null)
            {
                return BadRequest("Tournament data is required.");
            }
            var tournamentId = await _tournamentService.CreateTournamentAsync(dto, cancellationToken);
            return Ok(new { id = tournamentId });
        }
        [Authorize(Roles = "Organisator")]
        [HttpPut("matches/results")]
        public async Task<IActionResult> UpdateMatchResult([FromBody] UpdateMatchResultDto dto, CancellationToken ct)
        {
            try
            {
                await _tournamentService.UpdateMatchResultAsync(dto, ct);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpGet("compare-teams")]
        public async Task<IActionResult> Compare([FromQuery] int teamAId, [FromQuery] int teamBId, CancellationToken ct)
        {
            var result = await _tournamentService.CompareTeamsAsync(teamAId, teamBId, ct);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("{id}/view")]
        public async Task<ActionResult<TournamentViewDto>> GetTournamentView(int id, CancellationToken ct)
        {
            try
            {
                var result = await _tournamentService.GetPublicTournamentDataAsync(id, ct);
                return Ok(result);
            }
            catch (KeyNotFoundException)
            {
        
            return NotFound($"Турнир с ID {id} не существует.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [Authorize(Roles = "Player")]
        [HttpPost("teams/create-by-player")]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamRequestDto request, CancellationToken ct)
        {
            if (request == null)
            {
                return BadRequest("Тело запроса не может быть пустым.");
            }

            if (string.IsNullOrWhiteSpace(request.TeamName))
            {
                return BadRequest("Название команды обязательно.");
            }

            if (request.TournamentId <= 0 || request.PlayerId <= 0)
            {
                return BadRequest("Некорректный ID турнира или игрока.");
            }

            try
            {
                var teamId = await _tournamentService.PlayerCreatesTeamAsync(
                request.TeamName,
                request.TournamentId,
                request.PlayerId,
                ct);

            return Ok(new { TeamId = teamId, Message = "Команда создана успешно" });
            }
            catch (KeyNotFoundException ex)
            {              
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            { 
                return StatusCode(500, $"Внутренняя ошибка: {ex.Message}");
            }
        }

        
    }
}