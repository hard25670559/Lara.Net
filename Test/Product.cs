using Lara.Net.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Product : Model
    {

        public int Price { get; set; }
        public string Name { get; set; }

    }
}
