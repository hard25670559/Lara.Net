using Lara.Net.Core.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class LikeSession : IAuthAdapter
    {

        public static LoginStatus SessionLoginStatus = LoginStatus.LOGIN_ERROR;
        public static AuthMember SessionMember = new AuthMember
        {
            Account = "Test",
            Password = "Test",
        };

        public LoginStatus LoginStatus
        {
            get
            {
                return LikeSession.SessionLoginStatus;
            }
            set
            {
                LikeSession.SessionLoginStatus = value;
            }
        }
        public AuthMember Member
        {
            get
            {
                return LikeSession.SessionMember;
            }
            set
            {
                LikeSession.SessionMember = value;
            }
        }

    }

}
