using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace COVIDHelp.ViewModels
{
    public class SignUpPageViewModel : BaseViewModel
    {
        public User UserR { get; set; }
        public DelegateCommand ButtonConfirmCommand { get; set; }
        public DelegateCommand ButtonEyeClickedCommand { get; set; }
        public ImageSource ImageModel { get; set; }
        public bool IsVisibleList { get; set; } = false;
       
        public bool IsVisible { get; set; }
        public SignUpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            UserR = new User()
            {
                Nombres = "Rafael",
                Apellidos = "Fernandez",
                Cedula = 12345678912,
                Sexo = "M",
                SelfBiography = "Comiendo habichuela con dulce",
                Correo = "castillo@gmail.com",
                Password = "123456",
                Latitude = "18.5167",
                Longitude = "18.5167"

            };
            IsVisible = true;
            ImageModel = "eyeW.png";
            ButtonConfirmCommand = new DelegateCommand(async () => {
                    if (String.IsNullOrEmpty(UserR.Nombres) && String.IsNullOrEmpty(UserR.Correo) && String.IsNullOrEmpty(UserR.Password) && String.IsNullOrEmpty(UserR.RepeatPassword))
                    { await dialogService.DisplayAlertAsync("ALERT!", "THERE ARE EMPTY FIELDS", "Ok"); }
                    else if (String.IsNullOrEmpty(UserR.Correo))
                    { await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD EMAIL ADDRESS IS EMPTY", "Ok"); }
                    else if (String.IsNullOrEmpty(UserR.Nombres)) { await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD NAME IS EMPTY", "Ok"); }
                    else if (String.IsNullOrEmpty(UserR.Password)) { await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD PASSWORD IS EMPTY", "Ok"); }
                    else if (String.IsNullOrEmpty(UserR.RepeatPassword)) { await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD REPEAT PASSWORD IS EMPTY", "Ok"); }
                    else if (UserR.Password != UserR.RepeatPassword) { await App.Current.MainPage.DisplayAlert("ALERT!", "THE PASSWORD ARE NOT EQUAL", "OK"); }
                    else
                    {
                        PostUser(UserR);
                        await NavigateToSelectedSignUp();
                    }
                });
            ButtonEyeClickedCommand = new DelegateCommand(() => {

                VisiblePassWord();
            });

        }
    
        async Task NavigateToSelectedSignUp()
        {
            var param = new NavigationParameters();
            param.Add($"{nameof(User)}", UserR);
            await navigationService.NavigateAsync(new Uri($"{NavigationConstants.NeededTabbedPage}{NavigationConstants.NeededHomePage}", UriKind.Relative),param);
        }
        void PostUser(User user)
        {
            apiCovitServices.PostUser(user);
        }
        void VisiblePassWord()
        {
                ImageModel = !IsVisible ? "eyeW.png" : "eyeW_off";
                IsVisible = !IsVisible;
        }
    }
   
}
