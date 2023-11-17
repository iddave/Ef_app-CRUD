using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app
{
    internal class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Type { get; set; }
        public string ContactInfo { get; set; }

        public List<Contract> Contracts { get; set; } = new();/* = new List<Contract>();*/

        
    }
}
