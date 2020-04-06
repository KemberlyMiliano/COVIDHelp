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

        public async Task<User> ValidateUser([Body] User user)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            var users = await getRequest.ValidateUser(user);
            return users;
        }
    }

}
