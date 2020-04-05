using COVIDHelp.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
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
        public bool IsVisible { get; set; }
        public SignUpPageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            UserR = new User();
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
                        NavigateToSelectedSignUp();
                    }
                });
            ButtonEyeClickedCommand = new DelegateCommand(() => {

                VisiblePassWord();
            });

        }
        async Task NavigateToSelectedSignUp()
        {
            await navigationService.NavigateAsync(new Uri($"/SelectedSignUpPage", UriKind.Relative));
        }
        void VisiblePassWord()
        {
                ImageModel = !IsVisible ? "eyeW.png" : "eyeW_off";
                IsVisible = !IsVisible;
        }
    }
   
}
