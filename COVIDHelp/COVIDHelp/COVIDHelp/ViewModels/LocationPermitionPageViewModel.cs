using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace COVIDHelp.ViewModels
{
    public class LocationPermitionPageViewModel : BaseViewModel,IInitialize
    {
        public DelegateCommand ConfirmCommand { get; set; }
        public Locations GetLocation { get; set; } = new Locations();
        public User User { get; set; }
        public LocationPermitionPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            ConfirmCommand = new DelegateCommand(async () =>
            {
                try
                {
                    await GetPermisson();
                    var param = new NavigationParameters
                    {
                        { $"{Constants.IdKey}", User }
                    };
                    param.Add("Location", GetLocation);
                    await navigationService.GoBackAsync(param);
                }
                catch (Exception)
                {
                    await dialogService.DisplayAlertAsync("Ubicacion desactivada", "Para poder continuar por favor active la ubicacion de su celular.", "OK");
                }

            });
        }
        async Task GetPermisson()
        {
            User = await apiCovitServices.FindUser("phone", Setting.PhoneSetting, Setting.Token);
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

        public void Initialize(INavigationParameters parameters)
        {
            var param = parameters[Constants.IdKey] as User;
            User = param;
        }
    }
}
