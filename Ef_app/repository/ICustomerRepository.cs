using Ef_app.cmd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app.repository
{
    internal interface ICustomerRepository : IGenericRepository<Customer, int>
    {
        List<Customer> FindByName(string name);
    }
}
