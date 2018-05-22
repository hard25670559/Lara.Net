using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Auth
{

    /// <summary>
    /// 
    /// </summary>
    public interface IAuthAdapter
    {

        LoginStatus LoginStatus { get; set; }

        AuthMember Member { get; set; }


    }
}
