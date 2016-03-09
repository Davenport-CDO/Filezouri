using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy;
using Nancy.Authentication.Basic;
using Nancy.Security;

namespace Filezouri.App
{
    public class BasicUserValidator : IUserValidator
    {
        public IUserIdentity Validate(string username, string password)
        {
            if (username == Configuration.Current.Username && password == Configuration.Current.Password)
            {
                return new BasicUserIdentity
                {
                    Claims = new[] {"Upload"},
                    UserName = username
                };
            }
            else
            {
                return null;
            }
        }
    }
}
