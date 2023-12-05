using System.ComponentModel.DataAnnotations;

namespace OAuthService.Shared;
public class RegisterModel
{
	[Required(ErrorMessage = "Email is required")]
	public string? Email { get; set; }

	[Required(ErrorMessage = "Password is required")]
	[StringLength(16, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
	public string? Password { get; set; }

	[Required(ErrorMessage = "Confirm password is required")]
	[Compare(nameof(Password), ErrorMessage = "Password and Confirm Password do not match")]
	public string? ConfirmPassword { get; set; }
}
