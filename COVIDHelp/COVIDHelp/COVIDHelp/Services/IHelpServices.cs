using COVIDHelp.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public interface IHelpServices
    {
        [Post("/api/Helps")]
        Task<Help> PostHelp([Body] Help help, [Header("Authorization")] string token);

        [Put("/api/Helps")]
        Task<Help> PutHelp([Body] Help help, [Header("Authorization")] string token);

        [Get("/api/Helps/ByStatus/{type}/{status}")]
        Task<List<Help>> GetHelp(string type, string status, [Header("Authorization")] string token);

        [Get("/api/Helps/History/{type}/{id}")]
        Task<List<Help>> GetHelpID(string type, int id, [Header("Authorization")] string token);

    }
}
