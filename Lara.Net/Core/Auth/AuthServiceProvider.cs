using Lara.Net.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Lara.Net.Core.Auth
{
    public class AuthServiceProvider : IAuth
    {

        private Repository<AuthMember> MemberRepository = new Repository<AuthMember>();
        private IAuthAdapter AuthAdapter;

        public AuthServiceProvider(IAuthAdapter authAdapter)
        {
            this.AuthAdapter = authAdapter;
        }

        public AuthMember GetLoginMemberInfo()
        {
            return this.AuthAdapter.Member;
        }

        public bool IsLogin()
        {
            return this.AuthAdapter.LoginStatus == LoginStatus.LOGIN_SUCCESS;
        }

        public bool IsSelf(AuthMember member)
        {
            return this.AuthAdapter.Member.Account == member.Account && this.AuthAdapter.Member.Password == member.Password;
        }

        public LoginStatus Login(AuthMember member)
        {
            bool exist = this.MemberRepository.Read().Where(w => w.Account == member.Account && w.Password == member.Password).Count() == 1;

            this.AuthAdapter.LoginStatus = LoginStatus.LOGIN_ERROR;

            if (exist)
            {
                this.AuthAdapter.LoginStatus = LoginStatus.LOGIN_SUCCESS;
            }

            return this.AuthAdapter.LoginStatus;
        }

        public void Logout()
        {
            this.AuthAdapter.Member = null;
            this.AuthAdapter.LoginStatus = LoginStatus.NOT_YET;
        }
    }
}
