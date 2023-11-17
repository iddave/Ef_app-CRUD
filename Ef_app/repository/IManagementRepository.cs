using Ef_app.cmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app.repository
{
    internal interface IManagementRepository : IGenericRepository<Management, int>
    {
        List<Management> FindByName(string name);
    }
}
