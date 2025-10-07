using System.ComponentModel.DataAnnotations;

namespace Aurex_Infrastructure.DTO.AccountDtos
{
    public record LoginDto
    {
        [Required(ErrorMessage ="Emails is Required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; init; } = null!;
        [Required(ErrorMessage ="Password iS Required")]
        public string Password { get; init; } = null!;
    }
}
