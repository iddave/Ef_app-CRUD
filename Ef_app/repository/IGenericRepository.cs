using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app.cmd
{
    internal interface IGenericRepository<T, ID>
    {
        T FindById(ID id);
        List<T> FindAll();
        T Save(T entity);
        T DeleteById(ID id);
    }
}
