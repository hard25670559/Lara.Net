﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class CustomerRepository : Lara.Net.Core.Repository.Repository<Customer>
    {

        public CustomerRepository() : base(db: new DB())
        {

        }

    }
}
