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

            RegisterObjectConfig.ObjectContainer = new Dictionary<string, Type>
            {
                { "test", typeof(Lara.Net.Core.ObjectContainer.Test) },
            };

            ObjectContainer objectContainer = new ObjectContainer();

            objectContainer.GetObject(RegisterObjectConfig.ObjectContainer["test"].ToString());

            string s = objectContainer.GetMethod("CCC") as string;

        }
    }
}
