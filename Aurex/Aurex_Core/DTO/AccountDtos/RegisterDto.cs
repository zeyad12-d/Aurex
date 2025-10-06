namespace Aurex_Infrastructure.DTO.AccountDtos
{
    public record RegisterDto
    {
        public string UserName { get; init; } = null!;

        public string Email { get; init; } = null!;

        public string Password { get; init; } = null!;

        public string phoneNumber { get; init; } = null!;

    }
}
