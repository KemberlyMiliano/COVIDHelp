using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public interface ICovidUserServices
    {

        [Post("/api/Users/Evaluate")]
        Task<User> EvaluateVoluntary([Body] User user, [Header("Authorization")] string token);
        [Post("/api/Token/ForgotPassword")]
        Task<User> ForgotPassword([Body] User user, [Header("Authorization")] string token);

        [Get("/api/Users")]
        Task<List<User>> GetUser([Header("Authorization")] string token);
        [Get("/api/Users​/ValidateCode​/{number}​/{code}")]
        Task<User> ValidateCode(long number, int code, [Header("Authorization")] string token);

        [Get("/api/Users/SendActivationCode")]
        Task<User> SendCodePhone(int phone, [Header("Authorization")] string token);
        [Put("/api/Users")]
        Task<User> UpdateUser([Body] User user, [Header("Authorization")] string token);
        [Post("/api/Users/Photo/{phone}")]
        Task<HttpResponseMessage> PostPhoto(long phone, [Body] User user, [Header("Authorization")] string token);
        [Get("/api/Users/{type}/{number}")]
        Task<User> FindUser(string type, long number, [Header("Authorization")] string token);
    }
}
