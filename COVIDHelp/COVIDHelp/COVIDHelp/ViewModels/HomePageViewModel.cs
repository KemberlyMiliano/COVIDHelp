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
using System.Linq;
using System.Runtime.CompilerServices;

namespace COVIDHelp.ViewModels
{
    public class HomePageViewModel : BaseViewModel, IInitialize
    {
        public DelegateCommand ShowDialogCommand { get=>new DelegateCommand(() =>
        {
            dialog.ShowDialog("EmergencyPage", CloseDialogCallback);
        });}
        public DelegateCommand ShowFormulary { get => new DelegateCommand(async () =>
        {
            await OpenRideShareAsync();
        });
        }
        public DelegateCommand<string> GoToMaps { get => new DelegateCommand<string>(async (filtrar) =>
        {

            filtrar.SaveString("status");
            await navigationService.NavigateAsync(new Uri(NavigationConstants.MapsPage, UriKind.Relative));
        });}
        public DelegateCommand GoToMedicalAssintence { get =>  new DelegateCommand(async () =>
        {
            await navigationService.NavigateAsync(new Uri(NavigationConstants.MedicalAssistenceRequestPage, UriKind.Relative));
        });  }
        public DelegateCommand GoToIdentification { get =>new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.IdentificationPage, UriKind.Relative));
            }); }
        public DelegateCommand PermissionsCommand { get;set;}
        public bool IsVoluntary { get; set; }
        
        public User User { get; set; }
       private readonly IDialogService dialog;
        
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices,IHelpServices helpServices, IDialogService dialog) : base(navigationService, dialogService, userServices,helpServices)
        {
            this.dialog = dialog;
            PermissionsCommand = new DelegateCommand(async () =>
            {
                var user = await userServices.FindUser(Constants.IdKey, Setting.Id, Setting.Token);
                User = user;
                if (!IsNotConnected)
                {
                    await NavigateToPermisson();
                    User = await userServices.UpdateUser(User, Setting.Token);
                }
            });
            PermissionsCommand.Execute();

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
                var param = new NavigationParameters
                    {
                        { $"{Constants.IdKey}", User }
                    };
                await navigationService.NavigateAsync(new Uri($"{NavigationConstants.LocationPermitionPage}", UriKind.Relative), param,false);
            }
            else
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Default);
                var location = await Geolocation.GetLocationAsync(request);
               
                if (location != null)
                {
                    User.Latitude = $"{location.Latitude}";
                    User.Longitude = $"{location.Longitude}";
                    var address = (await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude)).FirstOrDefault();
                    User.Address = $"{address.CountryName},{address.AdminArea},{address.SubAdminArea},{address.Locality},{address.Thoroughfare}";

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

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(Constants.IdKey))
            {
                User param = parameters[Constants.IdKey] as User;
                PermissionsCommand.Execute();
                if (param != null)
                {
                    User = param;
                }

            }
        }
    }
}
