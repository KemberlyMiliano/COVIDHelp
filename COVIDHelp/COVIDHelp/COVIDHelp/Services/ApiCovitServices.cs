using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
<<<<<<< HEAD
    public class ApiCovitServices : IApiCovitServices
    {
        public async Task PostUser([Body] User user)
        {
            var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            await getRequest.PostUser(user);
        }
    }
=======

>>>>>>> 92ec9d4b65bc944937f19a504740f667ef5e26e1
}
