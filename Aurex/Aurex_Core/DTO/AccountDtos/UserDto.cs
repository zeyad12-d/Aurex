﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.DTO.AccountDtos
{
    public record UserDto
    {
     
            public string Id { get; set; }
            public string Email { get; set; }
            public string UserName { get; set; }
    }
}

