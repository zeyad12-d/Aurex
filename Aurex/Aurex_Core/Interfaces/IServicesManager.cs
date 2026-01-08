
using Aurex_Core.Interfaces.ModelInterfaces;

namespace Aurex_Core.Interfaces
{
    public interface IServicesManager
    {
        IAccountServices AccountServices { get; }
        IEmployeeServices EmployeeServices { get; }
        IDepartmentService DepartmentService { get; }
       IDealsService DealsService { get; }

        IClientService ClientService { get; } 
    }
}
