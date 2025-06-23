using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTOs;

public class RegisterRequestDto
{
    [Required]
    [DataType(DataType.Password)]
    public required string Username { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    
    public string[] Roles { get; set; }
}