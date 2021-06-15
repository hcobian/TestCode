using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTaxes
{
    public class Products
    {
        public string sku { get; set; }
        public string  Name { get; set; }
        public decimal Amount { get; set; }
        public bool Imported { get; set; }
        public bool NoBaseTax { get; set; }
    }
}
