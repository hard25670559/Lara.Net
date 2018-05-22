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

        public static IRepository<Model> Repository()
        {
            
            List<object> list = new List<object>
            {
                { new CustomerRepository() },
            };

            IRepository<Model> repository = list[0] as IRepository<Model>;

            return repository;

        }

        static void Main(string[] args)
        {
            RegisterObjectConfig.DBConfig = typeof(DB);

            //IRepository<Model> repository = Program.Repository();

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


            ProductRepository productRepository = new ProductRepository();
            List<Product> products = productRepository.Read();

            productRepository.Update(2, new Product
            {
                Name = "change name2."
            });

            //IRepository<Product> productRepository = RepositoryFacotry.Create<Product>(); ;


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
            //productRepository.Update(1, new Product
            //{
            //    Price = 500,
            //});

            products = productRepository.Read();


        }
    }
}
