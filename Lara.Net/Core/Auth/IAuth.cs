using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lara.Net.Core.Auth
{
    public interface IAuth
    {

        bool IsLogin();

        bool IsSelf(AuthMember member);

        LoginStatus Login(AuthMember member);

        void Logout();

    }
}
