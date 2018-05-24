using Lara.Net.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Auth
{
    public class AuthMember : Model
    {

        //public Purview Purview { get; set; }

        public string Account { get; set; }
        public string Password { get; set; }

    }
}
