using System.ComponentModel.DataAnnotations;

namespace Telneting.IDP.Domain.DTOs;

public class LogoutDto
{
	[Required]
	public string Email { get; set; }
    
    [Required]
	public string Token { get; set; } 
}


public class EmployeeDto
{
    [Required(ErrorMessage = "Employee name is a required field.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Age is a required field.")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Position is a required field.")]
    public string Position { get; set; }

}

public class ResendConfirmationEmailDto
{
    [Required]
    public string Email { get; set; }
}