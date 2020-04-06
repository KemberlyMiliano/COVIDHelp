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

        public async Task<List<User>> GetUser()
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var users = await getRequest.GetUser();
            return users;
        }

        public async Task<User> PostUser([Body] User user)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            return await getRequest.PostUser(user);
        }

        public Task<User> SendCodePhone(long cedula)
        {
            throw new NotImplementedException();
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
