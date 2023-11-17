using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app
{
    internal class Management
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }

        public int ContractId { get; set; }
        public Contract? Contract { get; set; }
    }
}
