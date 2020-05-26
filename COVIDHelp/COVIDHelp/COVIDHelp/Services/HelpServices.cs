using COVIDHelp.Helpers;
using COVIDHelp.Models;
using MonkeyCache.FileStore;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace COVIDHelp.Services
{
    public class HelpServices: IHelpServices
    {
        public HelpServices()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyCacheKey;
        }

        public async Task<List<Help>> GetHelp(string type, string status, [Header(Constants.Authorization)] string token)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(nameof(GetHelp)))
                {
                    return Barrel.Current.Get<List<Help>>(key: nameof(GetHelp));
                }
                var getRequest = RestService.For<IHelpServices>(ConfigApi.UrlApi);
                var helps = await getRequest.GetHelp(type, status, token);
                Barrel.Current.Add(key: nameof(GetHelp), data: helps, expireIn: TimeSpan.FromDays(20));
                return helps;
            }
            catch (Exception)
            {
                return null;
            }

        }




        public async Task<Help> PostHelp([Body] Help help, [Header(Constants.Authorization)] string token)
        {
            try
            {
                var getRequest = RestService.For<IHelpServices>(ConfigApi.UrlApi);
                return await getRequest.PostHelp(help, token);

            }
            catch (Exception)
            {

                return help;
            }

        }


        public async Task<Help> PutHelp([Body] Help help, [Header(Constants.Authorization)] string token)
        {
            try
            {
                var getRequest = RestService.For<IHelpServices>(ConfigApi.UrlApi);
                return await getRequest.PutHelp(help, token);
            }
            catch (Exception)
            {

                return help;
            }

        }
        public Task<List<Help>> GetHelpID(string type, int id, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

    }
}
