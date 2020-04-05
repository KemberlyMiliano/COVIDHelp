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
        Task PostUser([Body]User user);
    }
}
