using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class ProductRepository : Lara.Net.Core.Repository.Repository<Product>
    {
        public ProductRepository() : base(new DB())
        {
        }
    }
}
