using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace COVIDHelp.ViewModels
{
    public class RegisterPageViewModel : BaseViewModel, INavigatedAware
    {
        public User UserR { get; set; } = new User();
        public DelegateCommand ButtonConfirmCommand { get; set; }
        public DelegateCommand ButtonEyeClickedCommand { get; set; }
        public DelegateCommand AddImageUserCommand { get; set; }
      
        public DelegateCommand GoToLocationPermissionPage { get; set; }
        public DelegateCommand AddPhotoCommand { get; set; }
        public ImageSource ImageModel { get; set; }
        public List<TypePicker> Genders { get; set; }
        public bool IsVisible { get; set; }
        private TypePicker selectedGender;
        public TypePicker SelectedGender
        {
            get
            {
                return selectedGender;
            }
            set
            {
                selectedGender = value;
                if (selectedGender != null)
                {
                    Gender(SelectedGender.Gender);
                }
            }
        }
        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            IsVisible = true;
            ImageModel = "eyeW.png";
            PickerGender();
            ButtonConfirmCommand = new DelegateCommand(async () =>
            {
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

            ButtonEyeClickedCommand = new DelegateCommand(() =>
            {
                VisiblePassWord();
            });

            AddImageUserCommand = new DelegateCommand(() =>
            {
                // a;adir la imagen del usuario (usar permisos de camara y galeria.)
            });


            GoToLocationPermissionPage = new DelegateCommand(async () =>
            {
                await NavigateToPermisson();
            });

        }
        async Task NavigateToPermisson()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
            if (status != PermissionStatus.Granted)
            {
                await navigationService.NavigateAsync(new Uri($"{NavigationConstants.LocationPermitionPage}", UriKind.Relative));
            }

        }
        public void PickerGender()
        {
            Genders = new List<TypePicker>() { };
            Genders.Add(new TypePicker { Gender = "Masculino" });
            Genders.Add(new TypePicker { Gender = "Femenino" });
        }
        public void Gender(string gender)
        {
            switch (gender)
            {
                case "Masculino":
                    UserR.Sexo = gender;
                    break;
                case "Femenino":
                    UserR.Sexo = gender;
                    break;
                default:
                    break;
            }
        }
        async Task NavigateToSelectedSignUp()
        {
            var param = new NavigationParameters();
            param.Add($"{nameof(User)}", UserR);
            await navigationService.NavigateAsync($"{NavigationConstants.HelpersMainPage}", param);
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
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Location"))
            {
                var param = parameters["Location"] as Locations;
                UserR.Latitude = $"{param.Lat}";
                UserR.Longitude = $"{param.Lng}";
            }
        }
    }

}
