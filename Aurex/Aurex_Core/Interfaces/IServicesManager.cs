
using Aurex_Core.Interfaces.ModleInterFaces;

namespace Aurex_Core.Interfaces
{
    public interface IServicesManager
    {
        IAccountServices AccountServices { get; }
        IEmployeeServices EmployeeServices { get; }
        IDepartmentService DepartmentService { get; }

    }
}
