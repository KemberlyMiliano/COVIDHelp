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
    public class HomePageViewModel : BaseViewModel, IInitialize
    {
        public DelegateCommand ShowDialogCommand { get; set; }
        public DelegateCommand ShowFormulary { get; set; }
        public DelegateCommand<string> GoToMaps { get; set; }
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



            GoToMaps = new DelegateCommand<string>(async (filtrar) =>
            {
                var status = filtrar == "supermarket" ? "Alimentos" : "Medicamentos";
                status.SaveString("status");
                filtrar.SaveString("type");
                string latitude = $"{User.Latitude}";
                string longitude = $"{User.Longitude}";
                latitude.SaveString("latitude");
                longitude.SaveString("longitude");
                await navigationService.NavigateAsync(new Uri(NavigationConstants.MapsPage, UriKind.Relative));
            });

            GoToIdentification = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.IdentificationPage, UriKind.Relative));
            });

            GoToMedicalAssintence = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.MedicalAssistenceRequestPage, UriKind.Relative), param);
            });

            GoToMedicalAssintence = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.MedicalAssistenceRequestPage, UriKind.Relative), param);
            });

            ShowDialogCommand = new DelegateCommand(() =>
            {
                dialog.ShowDialog("EmergencyPage", CloseDialogCallback);
            });

            ShowFormulary = new DelegateCommand(async () =>
            {
                await OpenRideShareAsync();
            });
        }

        public async Task<bool> OpenRideShareAsync()
        {
            return await Launcher.TryOpenAsync(NavigationConstants.FormularyNavigation);
        }

        void CloseDialogCallback(IDialogResult dialogResult)
        {

        }
        async Task NavigateToPermisson()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationAlways>();
            if (status != PermissionStatus.Granted)
            {
                await navigationService.NavigateAsync(new Uri($"{NavigationConstants.LocationPermitionPage}", UriKind.Relative));
            }
            else
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Default);
                var location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    User.Latitude = $"{location.Latitude}";
                    User.Longitude = $"{location.Longitude}";
                    User = await apiCovitServices.UpdateUser(User);
                }
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

        public  void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(Constants.PersonKey))
            {
                var param = parameters[Constants.PersonKey] as User;
                User = param;
            }

        }
    }
}
