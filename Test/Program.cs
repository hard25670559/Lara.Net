using Lara.Net.Core;
using Lara.Net.Core.Config;
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

            ObjectContainer objectContainer = new ObjectContainer();
            
            objectContainer.GetObject(typeof(Customer).ToString());
            
            //objectContainer.GetMethod("");

            CustomerRepository customerRepository = new CustomerRepository();

            List<Customer> customers = customerRepository.Read();

            customerRepository.Create(new Customer
            {
                Name = "Customer2"
            });
            
            customers = customerRepository.Read();


        }
    }
}
