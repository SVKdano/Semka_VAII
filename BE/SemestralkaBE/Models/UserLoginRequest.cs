using System.ComponentModel.DataAnnotations;

namespace SemestralkaBE.Models;

public class UserLoginRequest
{
    [Required, EmailAddress] 
    public string Email { get; set; } = string.Empty;
    
    [Required, MinLength(6, ErrorMessage = "Minimal length of password is 6 characters!")] 
    public string Password { get; set; } = string.Empty;
}