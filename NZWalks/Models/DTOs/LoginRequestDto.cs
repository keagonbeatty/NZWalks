using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTOs;

public class LoginRequestDto
{
    [Required]
    [DataType(DataType.EmailAddress)]
    public required string UserName { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}