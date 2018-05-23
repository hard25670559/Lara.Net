using Lara.Net.Core;
using Lara.Net.Core.Config;
using Lara.Net.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            RegisterObjectConfig.DBConfig = typeof(DB);

            Repository<Product> productRepository = new Repository<Product>();
            List<Product> products = productRepository.Read();

            productRepository.Create(new Product
            {
                Price = 50,
                Name = "恩災"
            });

            productRepository.Update(2, new Product
            {
                Name = "change name2."
            });

            products = productRepository.Read();


        }
    }
}
