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
        public async Task PostUser([Body] User user)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            await getRequest.PostUser(user);
        }
    }
}
