using Aurex_Core.Entites;
using Aurex_Core.Interfaces.ModleInterFaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Services.Services
{
    public sealed class AccountServices :IAccountServices
    {
       private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly SignInManager<User> _signInManager;
        private readonly IMapper mapper;
        public AccountServices(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,SignInManager<User> signInManager, IMapper mapper )
        {
            
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            this.mapper = mapper;
        }





    }
}
