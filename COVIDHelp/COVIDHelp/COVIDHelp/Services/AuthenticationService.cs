using COVIDHelp.Helpers;
using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    class AuthenticationService : IAuthenticationService
    {
        public async Task<HttpResponseMessage> LoginUser([Body] User user)
        {
            try
            {
                var getRequest = RestService.For<IAuthenticationService>(ConfigApi.UrlApi);
                var token = await getRequest.LoginUser(user);
                Setting.Token = $"{await token.Content.ReadAsStringAsync()}";
                return token;
            }
            catch (Exception err)
            {
                return null;
            }

        }

        public async Task<HttpResponseMessage> SignUpUser([Body] User user)
        {
            var getrequest = RestService.For<IAuthenticationService>(ConfigApi.UrlApi);
            try
            {
                var token = await getrequest.SignUpUser(user);
                Setting.Token = $"{await token.Content.ReadAsStringAsync()}";
                return token;
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
