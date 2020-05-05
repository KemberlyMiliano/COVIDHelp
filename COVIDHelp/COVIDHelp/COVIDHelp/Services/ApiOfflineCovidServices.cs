using COVIDHelp.Helpers;
using COVIDHelp.Models;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.Services
{
    public class ApiOfflineCovidServices
    {
        IApiCovitServices apiCovitServices = new ApiCovitServices();

        public async Task PostHelpOffline(Help help = null)
        {
            if (!Setting.IsNotConnected && !Barrel.Current.IsExpired(nameof(help)))
            {
                help = Barrel.Current.Get<Help>(nameof(help));
                Barrel.Current.Empty(nameof(help));
                await apiCovitServices.PostHelp(help, Setting.Token);
               
            }
            else
            { 
            if (help!=null)
            {
                Barrel.Current.Add(key: nameof(help), data: help, expireIn: TimeSpan.FromDays(30));
            }
        }


        }
    }
}
