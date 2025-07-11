namespace NZWalks.Models.DTOs;

public class WalkDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; }
    
    public RegionDto Region { get; set; }
    public DifficultyDto Difficulty { get; set; }
}