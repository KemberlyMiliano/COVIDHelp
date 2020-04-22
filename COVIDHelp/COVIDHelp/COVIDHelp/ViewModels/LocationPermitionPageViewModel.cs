using COVIDHelp.Helpers;
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
        public User User { get; set; }
        public LocationPermitionPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            ConfirmCommand = new DelegateCommand(async () =>
            {
                try
                {
                    await GetPermisson();
<<<<<<< HEAD
                    var param = new NavigationParameters
                    {
                        { $"{Constants.PersonKey}", User }
                    };
                    await navigationService.GoBackAsync(param);
=======
                    var param = new NavigationParameters();
                    param.Add("Location", GetLocation);
                    //await navigationService.GoBackAsync(param);
                    await navigationService.NavigateAsync(new Uri(NavigationConstants.AddHomePage, UriKind.Relative), param);
>>>>>>> e4f5924b5e8c999b5a9c99417a26b303cc82c7fb
                }
                catch (Exception)
                {
                    await dialogService.DisplayAlertAsync("Ubicacion desactivada", "Para poder continuar por favor active la ubicacion de su celular.", "OK");
                }

            });
        }
        async Task GetPermisson()
        {
            long cedula = 0;
            User = await apiCovitServices.FindUser(cedula.GetPreferencesInt("Cedula"));
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
}
