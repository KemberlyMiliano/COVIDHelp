using COVIDHelp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using COVIDHelp.Services;
using COVIDHelp.Helpers;
using Xamarin.Essentials;
using Xamarin.Auth;
using System.Net.Http;
using Newtonsoft.Json;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using System.Diagnostics;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace COVIDHelp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public User User { get; set; } = new User();
        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand ButtonSignUpCommand { get; set; }
        public DelegateCommand ButtonEyeClickedCommand { get; set; }
        public DelegateCommand ForgotPasswordCommand { get; set; }
        public DelegateCommand LoginWithGoogleCommand { get; set; }
        public DelegateCommand LoggedInCommand { get; set; }
        public ImageSource ImageModel { get; set; }
        public AuthNetwork AuthNetwork { get; set; }
        public bool IsBusy { get; set; } = false;
        public bool IsVisible { get; set; }
        public string Phone { get; set; }
        public bool IsAlertVisible { get; set; }

        private bool isEnable;
        public bool IsEnable
        {
            get
            {
                return isEnable;
            }
            set
            {
                isEnable = value;
            }
        }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {


            LogInCommand = new DelegateCommand(async () =>
            {
                    IsBusy = true;
                     User.Phone = Convert.ToInt64(Phone);
                    await ValidateUser();
                    IsBusy = false;
            });

            ButtonSignUpCommand = new DelegateCommand(async () =>
            {
                await NavigateToRegister();
            });


            ForgotPasswordCommand = new DelegateCommand(() =>
            {
            });

            //LoginWithGoogleCommand = new DelegateCommand(async () =>
            //{
            //    await LoginGoogleAsync(AuthNetwork);
            //});
        }
        async Task NavigateToRegister()
        {
            await navigationService.NavigateAsync(new Uri(NavigationConstants.RegisterPage, UriKind.Relative));
        }

        async Task ValidateUser()
        {
            try
            {
               var succes =  await apiCovitServices.LoginUser(User);
                if (succes.IsSuccessStatusCode)
                {
                    var user = await apiCovitServices.FindUser("phone", User.Phone, Setting.Token);
                    User = user;
                    Setting.PhoneSetting = User.Phone;
                    Setting.Id = user.Id;
                    var param = new NavigationParameters
                    {
                    { $"{nameof(User)}", user }
                    };

                    IsAlertVisible = false;
                        await navigationService.NavigateAsync($"{NavigationConstants.HelpersMainPage}");
                }
                else
                {
                    IsAlertVisible = true;
                }
            }
            catch (Exception)
            {
                IsAlertVisible = true;
            }
        }
        //        async Task LoginGoogleAsync(AuthNetwork authNetwork)
        //        {
        //            try
        //            {
        //                if (!string.IsNullOrEmpty(_googleService.ActiveToken))
        //                {
        //                    //Always require user authentication
        //                    _googleService.Logout();
        //                }

        //                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
        //                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
        //                {
        //                    switch (e.Status)
        //                    {
        //                        case GoogleActionStatus.Completed:

        //#if DEBUG
        //                            var googleUserString = JsonConvert.SerializeObject(e.Data);
        //                            Debug.WriteLine($"Google Logged in succesfully: {googleUserString}");
        //#endif

        //                            var socialLoginData = new NetworkAuthData
        //                            {
        //                                Id = e.Data.Id,
        //                                Logo = authNetwork.Icon,
        //                                Foreground = authNetwork.Foreground,
        //                                Background = authNetwork.Background,
        //                                Picture = e.Data.Picture.AbsoluteUri,
        //                                Name = e.Data.Name,
        //                            };

        //                            var param = new NavigationParameters();
        //                            param.Add("GoogleHelper", socialLoginData);
        //                            await navigationService.NavigateAsync(new Uri(NavigationConstants.HomePage, UriKind.Relative), param);
        //                            break;

        //                        case GoogleActionStatus.Canceled:
        //                            await dialogService.DisplayAlertAsync("Google Auth", "Canceled", "Ok");
        //                            break;

        //                        case GoogleActionStatus.Error:
        //                            await dialogService.DisplayAlertAsync("Google Auth", "Error", "Ok");
        //                            break;

        //                        case GoogleActionStatus.Unauthorized:
        //                            await dialogService.DisplayAlertAsync("Google Auth", "Unauthorized", "Ok");
        //                            break;
        //                    }

        //                    _googleService.OnLogin -= userLoginDelegate;
        //                };

        //                _googleService.OnLogin += userLoginDelegate;

        //                await _googleService.LoginAsync();
        //            }
        //            catch (Exception ex)
        //            {
        //                Debug.WriteLine(ex.ToString());
        //            }
        //        }
    }
}

