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
    public class UserServices:ICovidUserServices
    {
        public UserServices()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyCacheKey;
        }
        public async Task<User> UpdateUser([Body] User user, [Header(Constants.Authorization)] string token)
        {
            try
            {
                var getRequest = RestService.For<ICovidUserServices>(ConfigApi.UrlApi);
                return await getRequest.UpdateUser(user, token);
            }
            catch (Exception)
            {

                return user;
            }

        }

        public Task<User> ForgotPassword([Body] User user, [Header(Constants.Authorization)] string token)
        {
            var request = RestService.For<ICovidUserServices>(ConfigApi.UrlApi);
            var forgot = request.ForgotPassword(user, token);
            return forgot;
        }

        public Task<User> EvaluateVoluntary([Body] User user, [Header(Constants.Authorization)] string token)
        {
            var request = RestService.For<ICovidUserServices>(ConfigApi.UrlApi);
            var evalute = request.EvaluateVoluntary(user, token);
            return evalute;
        }

        public Task<User> ValidateCode(long phone, int code, [Header(Constants.Authorization)] string token)
        {
            var request = RestService.For<ICovidUserServices>(ConfigApi.UrlApi);
            var user = request.ValidateCode(phone, code, token);
            return user;
        }
        public Task<User> SendCodePhone(int phone, [Header(Constants.Authorization)] string token)
        {
            throw new NotImplementedException();
        }
        public async Task<List<User>> GetUser([Header(Constants.Authorization)] string token)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet && !Barrel.Current.IsExpired(nameof(GetUser)))
                {
                    return Barrel.Current.Get<List<User>>(key: nameof(GetUser));
                }
                var getRequest = RestService.For<ICovidUserServices>(ConfigApi.UrlApi);
                var user = await getRequest.GetUser(token);
                Barrel.Current.Add(key: nameof(GetUser), data: user, expireIn: TimeSpan.FromDays(20));
                return user;
            }
            catch (Exception)
            {

                return null;
            }

        }
        public async Task<User> FindUser(string type, long phone, [Header(Constants.Authorization)] string token)
        {
            User user = null;
           var getRequest = RestService.For<ICovidUserServices>(ConfigApi.UrlApi);
           user = await getRequest.FindUser(type, phone, token);
           Barrel.Current.Add(key: nameof(FindUser), data: user, expireIn: TimeSpan.FromDays(20));
           return user;
        }

        public Task<HttpResponseMessage> PostPhoto(long phone, [Body] User user, [Header("Authorization")] string token)
        {
            var request = RestService.For<ICovidUserServices>(ConfigApi.UrlApi);
            var photo = request.PostPhoto(phone, user, token);
            return photo;
        }
    }
}
