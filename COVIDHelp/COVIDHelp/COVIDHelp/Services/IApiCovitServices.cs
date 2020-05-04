using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public interface IApiCovitServices
    {

        [Post("/api/Token/Login")]
        Task<HttpResponseMessage> LoginUser([Body]User user);
        [Post("/api/Token/SignUp")]
        Task<HttpResponseMessage> SignUpUser([Body]User user);
        [Post("/api/Users/Evaluate")]
        Task<User> EvaluateVoluntary([Body]User user, [Header("Authorization")] string token);
        [Post("/api/Token/ForgotPassword")]
        Task<User> ForgotPassword([Body]User user, [Header("Authorization")] string token);

        [Get("/api/Users")]
        Task<List<User>> GetUser([Header("Authorization")] string token);
        [Get("/api/Users​/ValidateCode​/{number}​/{code}")]
        Task<User> ValidateCode(long number, int code, [Header("Authorization")] string token);

        [Get("/api/Users/{type}/{number}")]
        Task<User> FindUser(string type, long number, [Header("Authorization")] string token);

        [Get("/api/Users/SendActivationCode")]
        Task<User> SendCodePhone(int phone, [Header("Authorization")] string token);

        [Put("/api/Users")]
        Task<User> UpdateUser([Body] User user, [Header("Authorization")] string token);

        [Post("/api/Helps")]
        Task<Help> PostHelp([Body] Help help, [Header("Authorization")] string token);

        [Put("/api/Helps")]
        Task<Help> PutHelp([Body] Help help, [Header("Authorization")] string token);

        [Get("/api/Helps/ByStatus/{type}/{status}")]
        Task<List<Help>> GetHelp(string type,string status,[Header("Authorization")] string token);

        [Get("/api/Helps/History/{type}/{id}")]
        Task<List<Help>> GetHelpID(string type, int id,[Header("Authorization")] string token);

    }
}

