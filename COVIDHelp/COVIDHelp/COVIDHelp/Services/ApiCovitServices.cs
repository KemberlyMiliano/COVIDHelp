using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public class ApiCovitServices : IApiCovitServices
    {
        public async Task<User> FindUser(Int64 cedula)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.FindUser(cedula);
        }

        public async Task<List<Help>> GetHelp()
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var helps = await getRequest.GetHelp();
            return helps;
        }

        public async Task<List<Help>> GetHelpActive()
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var helps = await getRequest.GetHelpActive();
            return helps;
        }

        public async Task<List<Help>> GetHelpCompletado()
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var helps = await getRequest.GetHelpCompletado();
            return helps;
        }

        public async Task<List<Help>> GetHelpProcess()
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var helps = await getRequest.GetHelpProcess();
            return helps;
        }

        public async Task<List<User>> GetUser()
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var help = await getRequest.GetUser();
            return help;
        }

        public async Task<Help> PostHelp([Body] Help help)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.PostHelp(help);
        }

        public async Task<User> PostUser([Body] User user)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.PostUser(user);
        }

        public async Task<Help> PutHelp([Body] Help help)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.PutHelp(help);
        }

        public Task<User> SendCodePhone(long cedula)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUser([Body] User user)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.UpdateUser(user);
        }

        public async Task<User> ValidateUser([Body] User user)
        {
            User users = null;
            try
            {
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
                users = await getRequest.ValidateUser(user);
                return users;
            }
            catch (Exception)
            {

                return users;
            }
        }
    }

}
