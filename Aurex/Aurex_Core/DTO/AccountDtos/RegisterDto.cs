using System.ComponentModel.DataAnnotations;

namespace Aurex_Core.DTO.AccountDtos
{
    public record RegisterDto
    {
        [Required(ErrorMessage = "UserName is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "UserName must be between 3 and 50 characters.")]
        public string UserName { get; init; } = null!;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; init; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; init; } = null!;

        [Required(ErrorMessage = "ConfirmedPassword is required.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmedPassword { get; init; } = null!;

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; init; } = null!;
    }
}
