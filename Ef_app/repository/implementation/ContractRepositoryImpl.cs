using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app.repository.implementation
{
    internal class ContractRepositoryImpl : IContractRepository
    {
        SampleContextFactory dbContextFactory = new SampleContextFactory();
        public Contract DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contract> FindAll()
        {
            throw new NotImplementedException();
        }

        public Contract FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Contract> FindByPrice(double price)
        {
            throw new NotImplementedException();
        }

        public Contract Save(Contract entity)
        {
            throw new NotImplementedException();
        }
    }
}
