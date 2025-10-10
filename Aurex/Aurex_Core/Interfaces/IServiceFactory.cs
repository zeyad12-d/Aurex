using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurex_Core.Interfaces
{
    public interface IServiceFactory
    {
        T CreateService<T>() where T : notnull;
    }
}
