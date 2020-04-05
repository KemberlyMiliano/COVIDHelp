using COVIDHelp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public User UserR { get; set; }
        public DelegateCommand ButtonLogInCommand { get; set; }
        public DelegateCommand ButtonSignUpCommand { get; set; }
        public DelegateCommand ButtonEyeClickedCommand { get; set; }
        public ImageSource ImageModel { get; set; }
        public bool IsVisible { get; set; }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            UserR = new User();
            IsVisible = true;
            ImageModel = "eyeW.png";

            ButtonLogInCommand = new DelegateCommand(async () => {
                if (String.IsNullOrEmpty(UserR.Correo) && String.IsNullOrEmpty(UserR.Password))
                {
                    await dialogService.DisplayAlertAsync("ALERT!", "THERE ARE EMPTY FIELDS", "Ok");
                  
                }
                else if (String.IsNullOrEmpty(UserR.Correo))
                {
                    await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD EMAIL ADDRESS IS EMPTY", "Ok");
                }
                else if (String.IsNullOrEmpty(UserR.Password))
                {
                    await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD PASSWORD IS EMPTY", "Ok");
                }
                else
                {
                   //Aqui va la pagina a donde sera dirigido el usuario registrado.
                }
            });

            ButtonSignUpCommand = new DelegateCommand(async () => {

                NavigateToRegister();
            });
            
            ButtonEyeClickedCommand = new DelegateCommand(() => {

                VisiblePassWord();
            });
        }

        async Task NavigateToRegister()
        {
            await navigationService.NavigateAsync(new Uri($"/SignUpPage", UriKind.Relative));
        }

        void VisiblePassWord()
        {
                ImageModel = !IsVisible ? "eyeW.png" : "eyeW_off.png";
                IsVisible = !IsVisible;
        }
    }
}
