namespace Backend.Presentation.DTOs
{
    public record TeamComparisonDto
    (
        string TeamAName,
        string TeamBName,
        double TeamAPower,
        double TeamBPower,
        double Difference
    );
    
}
