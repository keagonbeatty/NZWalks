using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTOs
{
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be at least 3 characters long.")]
        [MaxLength(3, ErrorMessage = "Code has to be maximumn 3 characters long.")]
        public required string Code { get; set; }
        
        [Required]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        [MaxLength(3, ErrorMessage = "Name has to be maximumn 3 characters long.")]
        public required string Name { get; set; }
        
        
        public string? RegionImageUrl { get; set; }
    }
}
