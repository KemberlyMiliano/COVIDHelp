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

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            IsVisible = true;
            ImageModel = "eyeW.png";

            LogInCommand = new DelegateCommand(async () => {
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

            ButtonSignUpCommand = new DelegateCommand(async () => {

                await NavigateToRegister();
            });
            
            ButtonEyeClickedCommand = new DelegateCommand(() => {

                VisiblePassWord();
            });
            ForgotPasswordCommand = new DelegateCommand(() => {
                //enviar correo con sus datos obtenidos del API.
            });
            LoginWithGoogleCommand = new DelegateCommand(() => {
                // Logearse con Google.
            });

        }

        async Task NavigateToRegister()
        {
            await navigationService.NavigateAsync(new Uri($"/SignUpPage", UriKind.Relative));
        }
        async Task ValidateUser()
        {
            try
            {
                var user = await apiCovitServices.ValidateUser(User);
                if (user != null)
                {
                    User = user;
                    User.Cedula.SaveInt("Cedula");
                    var param = new NavigationParameters
                    {
                    { $"{nameof(User)}", user }
                    };
                    await navigationService.NavigateAsync($"{NavigationConstants.HelpersMainPage}", param);
                }
            }
            catch (Exception)
            {
                await dialogService.DisplayAlertAsync("incorrecta", "contraseña/correo incorrecta", "ok");
            }
        }
        void VisiblePassWord()
        {
                ImageModel = !IsVisible ? "eyeW.png" : "eyeW_off.png";
                IsVisible = !IsVisible;
        }
    }
}
