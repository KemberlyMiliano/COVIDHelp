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

namespace COVIDHelp.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public User User { get; set; } = new User();
        public DelegateCommand LogInCommand { get; set; }
        public DelegateCommand ButtonSignUpCommand { get; set; }
        public DelegateCommand ButtonEyeClickedCommand { get; set; }
        public ImageSource ImageModel { get; set; }
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
                    var user = await apiCovitServices.ValidateUser(User);
                    if (user == null)
                    {
                        await dialogService.DisplayAlertAsync("ALERT!", "Incorrect password/email", "Ok");
                    }
                    else
                    {
                        var param = new NavigationParameters();
                        param.Add($"{nameof(User)}", User);
                        await navigationService.NavigateAsync(new Uri($"{NavigationConstants.NeededTabbedPage}?selectedTab={NavigationConstants.HelpersHomePage}", UriKind.Absolute), param);
                    }
                   
                }
            });

            ButtonSignUpCommand = new DelegateCommand(async () => {

                await NavigateToRegister();
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
