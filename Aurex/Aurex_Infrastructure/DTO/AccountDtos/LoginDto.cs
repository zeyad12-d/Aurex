﻿namespace Aurex_Infrastructure.DTO.AccountDtos
{
    public record LoginDto
    {
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
    }
}
