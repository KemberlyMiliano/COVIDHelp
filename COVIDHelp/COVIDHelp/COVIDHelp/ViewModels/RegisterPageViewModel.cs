using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
        public DelegateCommand AddImageUserCommand { get; set; }
      
        public DelegateCommand GoToLocationPermissionPage { get; set; }
        public DelegateCommand AddPhotoCommand { get => AddImageUserCommand = new DelegateCommand(() =>
        {
        }); }
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
                    UserR.Gender = selectedGender.Gender;
                }
            }
        }
        public DelegateCommand PictureCommand { get; set; }
        readonly IAuthenticationService authenticationService;
        public RegisterPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices,IHelpServices helpServices,IAuthenticationService authenticationService) : base(navigationService, dialogService, userServices,helpServices)
        {
            this.authenticationService = authenticationService;
            IsVisible = true;
            PickerGender();
            PictureCommand = new DelegateCommand(async () =>
            {
                await TakeImage();
            });
            ButtonConfirmCommand = new DelegateCommand(async () =>
            {
                if (BaseUserValidator.Name.IsMatch(UserR.Name)&&BaseUserValidator.Email.IsMatch(UserR.Email)&& BaseUserValidator.Password.IsMatch(UserR.Password))
                { await dialogService.DisplayAlertAsync("ALERT!", "THERE ARE EMPTY FIELDS", "Ok"); }
                else if (BaseUserValidator.Email.IsMatch(UserR.Email))
                { await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD EMAIL ADDRESS IS EMPTY", "Ok"); }
                else if (BaseUserValidator.Name.IsMatch(UserR.Name))  await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD NAME IS EMPTY", "Ok");
                else if (String.IsNullOrEmpty(UserR.Password)) await dialogService.DisplayAlertAsync("ALERT!", "THE FIELD PASSWORD IS EMPTY", "Ok"); 
                else
                {
                  
                    await NavigateToSelectedSignUp();
                }
            });




            GoToLocationPermissionPage = new DelegateCommand(async () =>
            {
                await NavigateToPermisson();
            });

        }
        async Task TakeImage()
        {
            const string takePhoto = "Take photo";
            const string ChossePhoto = "Chosse photo";
            MediaFile file = null;
            var dialog = await dialogService.DisplayActionSheetAsync("What do you want?", null, "Cancel", takePhoto, ChossePhoto);
            switch (dialog)
            {
                case takePhoto:
                    {
                        file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { PhotoSize = PhotoSize.Medium });
                        UserR.ProfilePhoto = file.Path;
                        break;
                    }

                case ChossePhoto:
                    {
                        file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions { PhotoSize = PhotoSize.Medium });
                        UserR.ProfilePhoto = file.Path;
                        break;
                    }


                default:
                    break;
            }
        }
        async Task PostPhoto(User user)
        {

            if (UserR.ProfilePhoto != null)
            {
                await userServices.PostPhoto(user.Phone, user, Setting.Token);
            }
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
            Genders.Add(new TypePicker { Gender = $"{EGender.Male}" });
            Genders.Add(new TypePicker { Gender = $"{EGender.Female}" });
        }
        async Task NavigateToSelectedSignUp()
        {
             var request = await PostUser(UserR);
            if (request.IsSuccessStatusCode)
            {
                var param = new NavigationParameters
                {
                    { $"{nameof(User)}", UserR }
                };
                await navigationService.NavigateAsync($"{NavigationConstants.HelpersMainPage}", param);
            }
        }
        async Task<HttpResponseMessage> PostUser(User user)
        {
         return await authenticationService.SignUpUser(user);
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
