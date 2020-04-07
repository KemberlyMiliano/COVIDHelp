using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using COVIDHelp.Helpers;

using Xamarin.Essentials;
using System.Threading.Tasks;
using Prism.Services.Dialogs;

namespace COVIDHelp.ViewModels
{
    public class HomePageViewModel : BaseViewModel, INavigatedAware
    {
        public DelegateCommand ShowDialogCommand { get; set; }
        public DelegateCommand<string> GoToMaps { get; set; }
        public DelegateCommand EmergencyCommand { get; set; }
        public DelegateCommand GoToMedicalAssintence { get; set; }
        public DelegateCommand GoToIdentification { get; set; }
        public DelegateCommand PermissionsCommand { get; set; }
        public bool IsVoluntary { get; set; }
        public User User { get; set; }
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IDialogService dialog) : base(navigationService, dialogService, apiCovitServices)
        {
            var param = new NavigationParameters();
            PermissionsCommand = new DelegateCommand(async () =>
            {
                await NavigateToPermisson();
            });
            PermissionsCommand.Execute();
            GoToMaps = new DelegateCommand<string>(async (filtrar) =>
            {

                filtrar.SaveString("type");
                string latitude = $"{User.Latitude}";
                string longitude = $"{User.Longitude}";
                latitude.SaveString("latitude");
                longitude.SaveString("longitude");
                await navigationService.NavigateAsync(new Uri(NavigationConstants.MapsPage, UriKind.Relative));
            });

            GoToIdentification = new DelegateCommand(async () =>
<<<<<<< HEAD
                {
                    param.Add("User", User);
                    await navigationService.NavigateAsync(new Uri(NavigationConstants.IdentificationPage, UriKind.Relative), param);
                });

                GoToMedicalAssintence = new DelegateCommand(async () =>
                {
                    await navigationService.NavigateAsync(new Uri(NavigationConstants.MedicalAssistenceRequestPage, UriKind.Relative), param);
                });

                EmergencyCommand = new DelegateCommand(() =>
                {
                    PlacePhoneCall("911");
                });
            }
=======
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.IdentificationPage, UriKind.Relative), param);
            });

            GoToMedicalAssintence = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.MedicalAssistenceRequestPage, UriKind.Relative), param);
            });

            EmergencyCommand = new DelegateCommand(() =>
            {
                PlacePhoneCall("911");
            });
>>>>>>> 7f1b20b3298e5577215b9c4998b3c3b8aadd83eb

            ShowDialogCommand = new DelegateCommand(() =>
            {
                dialog.ShowDialog("EmergencyPage", CloseDialogCallback);
            });
        }

        void CloseDialogCallback(IDialogResult dialogResult)
        {

        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        async Task NavigateToPermisson()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
            if (status != PermissionStatus.Granted)
            {
                await navigationService.NavigateAsync(new Uri($"{NavigationConstants.LocationPermitionPage}", UriKind.Relative));
            }

        }
        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey($"User"))
            {
                var param = parameters[$"{nameof(User)}"] as User;
                User = param;

                var lat = await User.Latitude.Latitude();
                User.Latitude = lat != User.Latitude ? lat : User.Latitude;
                var lng = await User.Longitude.Longitude();
                User.Longitude = lng != User.Longitude ? lng : User.Longitude;
                User = await apiCovitServices.UpdateUser(User);
            }

        }
        public void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }

            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        async void ProcessException(Exception ex)
        {
            if (ex != null)
                await dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
        }

    }
}
