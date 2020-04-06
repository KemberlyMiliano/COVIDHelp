using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public interface IApiCovitServices
    {
        [Post("/api/Users")]
        Task<User>PostUser([Body]User user);
        [Post("/api/Users/Login")]
        Task<User> ValidateUser([Body]User user);
        [Get("/api/Users")]
        Task<List<User>> GetUser();
         [Get("/api/Users/{cedula}")]
        Task<User> FindUser(Int64 cedula); 
        [Get("/api/Users/SendActivationCode")]
        Task<User> SendCodePhone(Int64 cedula);
    }
}

