using Backend.Domain.Entities.Tournaments;

namespace Backend.Domain.Services
{
    public class TeamBalanceReport
    {
        public double TeamAPower { get; set; }
        public double TeamBPower { get; set; }
        public double Difference { get; set; }
        
    }
    public class BalanceService
    {
        public TeamBalanceReport EvaluateBalance(Team TeamA, Team TeamB)
        {
            var teamAPower = CalculateTeamPower(TeamA);
            var teamBPower = CalculateTeamPower(TeamB);
            var diff = Math.Abs(teamAPower - teamBPower);

            return new TeamBalanceReport
            {
                TeamAPower = Math.Round(teamAPower, 2),
                TeamBPower = Math.Round(teamBPower, 2),
                Difference = Math.Round(diff, 2)
            };
        }
        public double CalculateTeamPower(Team team)
        {
            if(team.Players == null || !team.Players.Any())
                return 0;
            return team.Players.Average(p => p.Deaths == 0 ? (p.Kills + p.Assists) : (double)(p.Kills + p.Assists) / p.Deaths);
        }
    }
}
