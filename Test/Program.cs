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

            //RegisterObjectConfig.ObjectContainer = new Dictionary<string, Type>
            //{
            //    { "test", typeof(Lara.Net.Core.ObjectContainer.Test) },
            //};
            //
            //ObjectContainer objectContainer = new ObjectContainer();
            //
            //objectContainer.GetObject(RegisterObjectConfig.ObjectContainer["test"].ToString());
            //
            //string s = objectContainer.GetMethod("CCC") as string;

            //ObjectContainer objectContainer = new ObjectContainer();
            //
            //objectContainer.GetObject(typeof(Customer).ToString());

            //objectContainer.GetMethod("");

            //CustomerRepository customerRepository = new CustomerRepository();
            //
            //List<Customer> customers = customerRepository.Read();
            //
            //customerRepository.Update(1, new Customer
            //{
            //    Name = "Change Name4."
            //});
            //
            //customers = customerRepository.Read();


            RegisterObjectConfig.RepositoryModelContainer = new Dictionary<string, Type>
            {
                { "product", typeof(Product) }
            };

            RegisterObjectConfig.DBConfig = typeof(DB);

            ProductRepository productRepository = new ProductRepository();
            //IRepository<Model> productRepository = RepositoryFacotry.Create("product");

            List<Product> products = productRepository.Read();

            //productRepository.Create(new Product
            //{
            //    Price = 100,
            //    Name = "PC",
            //});
            //
            //productRepository.Create(new Product
            //{
            //    Price = 200,
            //    Name = "Mouse",
            //});
            //
            productRepository.Create(new Product
            {
                Price = 666,
                Name = "666",
            });

            //productRepository.Update(1, new Product
            //{
            //    Price = 500,
            //});

            //products = productRepository.Read();


        }
    }
}
