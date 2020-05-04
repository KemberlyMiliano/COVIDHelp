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
    public class ApiCovitServices : IApiCovitServices
    {
        public async Task<User> FindUser(string type,long phone, [Header(Constants.Authorization)] string token)
        {
            User user = null;
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
                 user = await getRequest.FindUser(type, phone, token);
                return user;

        }
        public async Task<List<Help>> GetHelp(string type,string status,[Header(Constants.Authorization)] string token)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var helps = await getRequest.GetHelp(type,status,token);
            return helps;
        }

       
        public async Task<List<User>> GetUser([Header(Constants.Authorization)] string token)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var help = await getRequest.GetUser(token);
            return help;
        }

        public async Task<Help> PostHelp([Body] Help help, [Header(Constants.Authorization)] string token)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.PostHelp(help,token);
        }


        public async Task<Help> PutHelp([Body] Help help, [Header(Constants.Authorization)] string token)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.PutHelp(help,token);
        }

        public Task<User> SendCodePhone(int phone, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUser([Body] User user, [Header(Constants.Authorization    )] string token)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.UpdateUser(user,token);
        }

        public async Task<HttpResponseMessage> LoginUser([Body] User user)
        {
            try
            {
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
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
            var getrequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
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

        public Task<User> ForgotPassword([Body] User user, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

        public Task<User> EvaluateVoluntary([Body] User user, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

        public Task<User> ValidateCode(long phone, int code, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

        public Task<List<Help>> GetHelpID(string type, int id, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }
    }

}
