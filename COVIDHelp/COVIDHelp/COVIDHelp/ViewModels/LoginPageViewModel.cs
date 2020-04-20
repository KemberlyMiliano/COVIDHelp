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
        public ImageSource ImageModel { get; set; }
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

            LogInCommand = new DelegateCommand(async () =>
            {
                if (string.IsNullOrEmpty(User.Correo) && string.IsNullOrEmpty(User.Password))
                {
                    await dialogService.DisplayAlertAsync("ALERT!", "THERE ARE EMPTY FIELDS", "Ok");
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

            LoginWithGoogleCommand = new DelegateCommand(() =>
            {
                // Logearse con Google.
                GoogleLogin();
            });

        }

        async Task NavigateToRegister()
        {
            await navigationService.NavigateAsync(new Uri(NavigationConstants.SignUpPage, UriKind.Relative));
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

        void GoogleLogin()
        {
            var authenticator = new OAuth2Authenticator
                         (
                           "Google client ID",
                           "email profile",
                            new System.Uri("https://accounts.google.com/o/oauth2/auth"),
                            new System.Uri("https://www.google.com")
                          );

            authenticator.AllowCancel = true;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);

            authenticator.Completed += async (senders, obj) =>
            {
                if (obj.IsAuthenticated)
                {
                    var clientData = new HttpClient();

                    //call google api to fetch logged in user profile info 
                    var resData = await clientData.GetAsync("https://www.googleapis.com/oauth2/v3/userinfo?access_token=" + obj.Account.Properties["access_token"]);
                    var jsonData = await resData.Content.ReadAsStringAsync();

                    // deserlize the jsondata and intilize in GoogleAuthClass
                    GoogleAuth googleObject = JsonConvert.DeserializeObject<GoogleAuth>(jsonData);

                    //you can access following property after login
                    string email = googleObject.Email;
                    string photo = googleObject.Picture;
                    string name = googleObject.Name;
                }
                else
                {
                    //Authentication fail
                    // write the code to handle when auth failed
                }
            };
            authenticator.Error += onAuthError;
        }

        private void onAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            dialogService.DisplayAlertAsync("Google Authentication Error", e.Message, "OK");
        }
    }
}

