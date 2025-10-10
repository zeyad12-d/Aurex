using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModelInterFaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Services.Services.Manager
{
    public sealed class ServicesManager :IServicesManager
    {
       private readonly IServiceFactory _serviceFactory;
        private readonly Lazy<IAccountServices> _accountServices;
        public ServicesManager(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
            _accountServices = new Lazy<IAccountServices>(() => _serviceFactory.CreateService<IAccountServices>());
        }
        public IAccountServices AccountServices => _accountServices.Value;

    }
}
