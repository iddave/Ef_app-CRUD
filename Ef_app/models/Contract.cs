using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ef_app
{
    internal class Contract
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<Management> Managements { get; set; } = new();

        
    }
}
