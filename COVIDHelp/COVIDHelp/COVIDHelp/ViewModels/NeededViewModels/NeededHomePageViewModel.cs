using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace COVIDHelp.ViewModels.NeededViewModels
{
    public class NeededHomePageViewModel:BaseViewModel
    {
        public DelegateCommand GoToNeedHelp { get; set; }
        public NeededHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService):base(navigationService, dialogService)
        {
            this.navigationService = navigationService;
            GoToNeedHelp = new DelegateCommand(async () =>
            {
                await NavigateToNeed();
            });
        }
        async Task NavigateToNeed()
        {
            await navigationService.NavigateAsync(new Uri($"/SelectNeededPage",UriKind.Relative));
        }
        CancellationTokenSource cts;
        async Task OnGetCurrentLocation()
        {
            
            int accuracy = (int)GeolocationAccuracy.Default;
            try
            {
                var request = new GeolocationRequest((GeolocationAccuracy)accuracy);
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                var hola = FormatLocation(location);
            }
            catch (Exception ex)
            {
                var a = FormatLocation(null, ex);
            }
            finally
            {
                cts.Dispose();
                cts = null;
            }
        }
        string FormatLocation(Location location, Exception ex = null)
        {
            if (location == null)
            {
                return $"No se puede detectar la ubicación. Excepción: {ex?.Message ?? string.Empty}";
            }

            return
                $"Latitud: {location.Latitude}\n" +
                $"Longitud: {location.Longitude}\n" +
                $"Exactitud: {location.Accuracy}\n" +
                $"Fecha (UTC): {location.Timestamp:d}\n" +
                $"Hora (UTC): {location.Timestamp:T}" +
                $"Moking Provider: {location.IsFromMockProvider}";
        }
    }
}
