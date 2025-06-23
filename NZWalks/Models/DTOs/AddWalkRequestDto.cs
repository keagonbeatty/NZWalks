using System.ComponentModel.DataAnnotations;
using NZWalks.Models.Domain;

namespace NZWalks.Models.DTOs;

public class AddWalkRequestDto
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }
    [Required]
    [MaxLength(1000)]
    public required string Description { get; set; }
    [Required]
    [Range(0, 50)]
    public required double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; }
    [Required]
    public Guid DifficultyId { get; set; }
    [Required]
    public Guid RegionId { get; set; }
    
}