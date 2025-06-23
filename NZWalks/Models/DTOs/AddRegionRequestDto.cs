using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTOs
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to be minimum 3 or more characters.")]
        [MaxLength(3, ErrorMessage = "Code has to be minimum 3 or more characters.")]
        public required string Code { get; set; }
        
        [Required]
        [MaxLength(100, ErrorMessage = "Name has to be maximumn 100 or less characters.")]
        public required string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
