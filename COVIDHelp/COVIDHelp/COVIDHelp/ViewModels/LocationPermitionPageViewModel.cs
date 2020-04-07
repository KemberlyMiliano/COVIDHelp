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

namespace COVIDHelp.ViewModels
{
    public class LocationPermitionPageViewModel : BaseViewModel
    {
        public DelegateCommand ConfirmCommand { get; set; }
        public Locations GetLocation { get; set; } = new Locations();
        public LocationPermitionPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            ConfirmCommand = new DelegateCommand(async () => {
                try
                {
                    await GetPermisson();
                    var param = new NavigationParameters();
                    param.Add("Location", GetLocation);
                    await navigationService.GoBackAsync(param);
                }
                catch (Exception)
                {

                    await dialogService.DisplayAlertAsync("Ubicacion desactivada", "para poder continuar por favor active la ubicacion de su telefono", "Ok");
                }

            });
        }
       async Task GetPermisson()
        {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var location = await Geolocation.GetLocationAsync(request);
                if (location != null)
                {
                    GetLocation.Lat = location.Latitude;
                    GetLocation.Lng = location.Longitude;
                }

        }

    }
}
