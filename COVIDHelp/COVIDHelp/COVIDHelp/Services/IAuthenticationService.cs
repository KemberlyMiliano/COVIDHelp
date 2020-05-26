using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public interface IAuthenticationService
    {
        [Post("/api/Token/Login")]
        Task<HttpResponseMessage> LoginUser([Body] User user);
        [Post("/api/Token/SignUp")]
        Task<HttpResponseMessage> SignUpUser([Body] User user);
    }
}
