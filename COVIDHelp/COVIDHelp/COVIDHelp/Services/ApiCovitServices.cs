using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Views.HelpersViews;
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
    public class ApiCovitServices : IApiCovitServices
    {
        public ApiCovitServices()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyCacheKey;
        }
        public async Task<User> FindUser(string type,long phone, [Header(Constants.Authorization)] string token)
        {
            User user = null;
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(nameof(FindUser)))
                {
                    return Barrel.Current.Get<User>(key: nameof(FindUser));
                }
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
                user = await getRequest.FindUser(type, phone, token);
                 Barrel.Current.Add(key: nameof(FindUser), data: user, expireIn: TimeSpan.FromDays(20));
                return user;
            }
            catch (Exception)
            {

                return null;
            }


        }
        public async Task<List<Help>> GetHelp(string type,string status,[Header(Constants.Authorization)] string token)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(nameof(GetHelp)))
                {
                    return Barrel.Current.Get<List<Help>>(key: nameof(GetHelp));
                }
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
                var helps = await getRequest.GetHelp(type, status, token);
                Barrel.Current.Add(key: nameof(GetHelp), data: helps, expireIn: TimeSpan.FromDays(20));
                return helps;
            }
            catch (Exception)
            {
                return null;
            }

        }

       
        public async Task<List<User>> GetUser([Header(Constants.Authorization)] string token)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(nameof(GetHelp)))
                {
                    return Barrel.Current.Get<List<User>>(key: nameof(GetUser));
                }
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
                var user = await getRequest.GetUser(token);
                Barrel.Current.Add(key: nameof(GetUser), data: user, expireIn: TimeSpan.FromDays(20));
                return user;
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
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
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
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
                return await getRequest.PutHelp(help, token);
            }
            catch (Exception)
            {

                return help;
            }

        }

        public Task<User> SendCodePhone(int phone, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateUser([Body] User user, [Header(Constants.Authorization)] string token)
        {
            try
            {
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
                return await getRequest.UpdateUser(user, token);
            }
            catch (Exception)
            {

                return user;
            }

        }

        public async Task<HttpResponseMessage> LoginUser([Body] User user)
        {
            try
            {
                var getRequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
               var token = await getRequest.LoginUser(user);
                Setting.Token = $"{await token.Content.ReadAsStringAsync()}";
                return token;
            }
            catch (Exception err)
            {
                return null;
            }

        }

        public async Task<HttpResponseMessage> SignUpUser([Body] User user)
        {
            var getrequest = RestService.For<IApiCovitServices>(ConfigApi.UrlApi);
            try
            {
                var token = await getrequest.SignUpUser(user);
                Setting.Token = $"{await token.Content.ReadAsStringAsync()}";
                return token;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public Task<User> ForgotPassword([Body] User user, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

        public Task<User> EvaluateVoluntary([Body] User user, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

        public Task<User> ValidateCode(long phone, int code, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }

        public Task<List<Help>> GetHelpID(string type, int id, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }
    }

}
