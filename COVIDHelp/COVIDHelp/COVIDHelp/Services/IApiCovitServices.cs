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
        [Put("/api/Users")]
        Task<User> UpdateUser([Body] User user);

        [Post("/api/Helps")]
        Task<Help> PostHelp([Body] Help help);
        [Put("/api/Helps")]
        Task<Help> PutHelp([Body] Help help);
        [Get("/api/Helps")]
        Task<List<Help>> GetHelp();
        [Get("/api/Helps/ByStatus/Activo")]
        Task<List<Help>> GetHelpActive();
        [Get("/api/Helps/ByStatus/Proceso")]
        Task<List<Help>> GetHelpProcess();
        [Get("/api/Helps/ByStatus/Completado")]
        Task<List<Help>> GetHelpCompletado();



    }
}

