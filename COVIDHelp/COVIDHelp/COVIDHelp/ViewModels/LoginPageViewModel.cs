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
        //IGoogleClientManager _googleService = CrossGoogleClient.Current;
        public User User { get; set; } = new User();
        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand ButtonSignUpCommand { get; set; }
        public DelegateCommand ButtonEyeClickedCommand { get; set; }
        public DelegateCommand ForgotPasswordCommand { get; set; }
        public DelegateCommand LoginWithGoogleCommand { get; set; }
        public ImageSource ImageModel { get; set; }
        public AuthNetwork AuthNetwork { get; set; }
        public bool IsBusy { get; set; } = false;
        public bool IsVisible { get; set; }
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
            IsEnable = Setting.IsCheckRember() == true ? Setting.IsCheckRember() : false;
            IsVisible = true;
            IsAlertVisible = false;
            ImageModel = "eyeW.png";
            User.Correo = User.Correo.Recordar(IsEnable);

            AuthNetwork = new AuthNetwork()
            {
                Name = "Google",
                Icon = "googleLogin",
                Foreground = "#000000",
                Background = "#F8F8F8"
            };

            LogInCommand = new DelegateCommand(async () =>
            {
                if (string.IsNullOrEmpty(User.Correo) && string.IsNullOrEmpty(User.Password))
                {
                    await dialogService.DisplayAlertAsync("ALERT!", "THERE ARE EMPTY FIELDS", "OK");
                }
                else
                {
                    IsBusy = true;
                    await ValidateUser();
                    IsBusy = false;
                }
            });

            ButtonSignUpCommand = new DelegateCommand(async () =>
            {
                await NavigateToRegister();
            });

            ButtonEyeClickedCommand = new DelegateCommand(() =>
            {
                VisiblePassWord();
            });

            ForgotPasswordCommand = new DelegateCommand(() =>
            {
                //Enviar correo con sus datos obtenidos del API.
            });

            //LoginWithGoogleCommand = new DelegateCommand(async () =>
            //{
            //    await LoginGoogleAsync(AuthNetwork);
            //});
        }
        async Task NavigateToRegister()
        {
            await navigationService.NavigateAsync(new Uri(NavigationConstants.NumberPage, UriKind.Relative));
        }
        async Task ValidateUser()
        {
            try
            {
                var user = await apiCovitServices.ValidateUser(User);
                if (user != null)
                {
                    if (IsEnable)
                    {
                        IsEnable.IsRemeberme(User.Correo);
                    }
                    User = user;
                    User.Cedula.SaveInt("Cedula");
                    var param = new NavigationParameters
                    {
                    { $"{nameof(User)}", user }
                    };

                    IsAlertVisible = false;
                    await navigationService.NavigateAsync($"{NavigationConstants.HelpersMainPage}", param);
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
        void VisiblePassWord()
        {
            ImageModel = !IsVisible ? "eyeW.png" : "eyeW_off.png";
            IsVisible = !IsVisible;
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

