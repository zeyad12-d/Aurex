using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.Entites
{
    public class User:IdentityUser
    {
        public Employee Employee { get; set; }
    }
}
