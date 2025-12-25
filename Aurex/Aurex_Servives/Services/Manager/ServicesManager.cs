using Aurex_Core.Interfaces;
using Aurex_Core.Interfaces.ModleInterFaces;
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
        private readonly Lazy<IEmployeeServices> _employeeServices;
        private readonly Lazy<IDepartmentService> _DepartmentService; 
        private readonly Lazy<IDealsService> _dealsServices ;
        public ServicesManager(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
            _accountServices = new Lazy<IAccountServices>(() => _serviceFactory.CreateService<IAccountServices>());
            _employeeServices = new Lazy<IEmployeeServices>(() => _serviceFactory.CreateService<IEmployeeServices>());
            _DepartmentService = new Lazy<IDepartmentService>(() => _serviceFactory.CreateService<IDepartmentService>());
            _dealsServices = new Lazy<IDealsService>(() => _serviceFactory.CreateService<IDealsService>());
        }
        public IAccountServices AccountServices => _accountServices.Value;
        public IEmployeeServices EmployeeServices => _employeeServices.Value;

        public IDepartmentService DepartmentService => _DepartmentService.Value;
        public IDealsService DealsService => _dealsServices.Value; 

    }
}
