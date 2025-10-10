using Aurex_Core.Interfaces.ModelInterFaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.Interfaces
{
    public interface IServicesManager
    {
        IAccountServices AccountServices { get; }
    }
}
