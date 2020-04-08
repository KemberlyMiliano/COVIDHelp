using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using COVIDHelp.Helpers;
using Xamarin.Essentials;

namespace COVIDHelp.ViewModels
{
    public class RequestDetailPageViewModel : BaseViewModel,INavigatedAware
    {
        public Help Help { get; set; }
        public DelegateCommand GoToHistorial { get; set; }
        public DelegateCommand GoBack { get; set; }
        public DelegateCommand GoToMaps { get; set; }
        public RequestDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            var param = new NavigationParameters();

            GoBack = new DelegateCommand(async () =>
            {
                await navigationService.GoBackAsync();
            });

            GoToHistorial = new DelegateCommand(async () =>
            {
                await DisplayAction();
            });
            GoToMaps = new DelegateCommand(async () =>
            {
                double lat = double.Parse(Help.Posicion.Split(',')[0]);
                double lng = double.Parse(Help.Posicion.Split(',')[1]);
                await Map.OpenAsync(lat, lng, new MapLaunchOptions
                {
                    Name = Help.Nombre,
                    NavigationMode = Xamarin.Essentials.NavigationMode.Driving
                });
            });


        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var param = parameters["Helper"] as Help;
            Help = param;
        }
        public async Task DisplayAction()
        {
            bool action = await dialogService.DisplayAlertAsync("", "Estás seguro/a?", "Aceptar", "Cancelar");
            Int64 cedula = 0;
            string status = "";
            var id = cedula.GetPreferencesInt("Cedula");
            var user = await apiCovitServices.FindUser(id);
            if (action&&user!=null)
            {
                Help help = new Help
                {
                    Id = Help.Id,
                    NombreVoluntario = $"{user.Nombres} {user.Apellidos}",
                    CedulaVoluntario = user.Cedula,
                    TelefonoVoluntario = user.Telefono,
                    Status = "Proceso",
                    EmailVoluntario = user.Correo,
                    PosicionVoluntario = $"{user.Latitude}{user.Longitude}",
                    FechaEnviado = DateTime.Now,
                    Tipo= status.GetPreferences("status")

                };
                await apiCovitServices.PutHelp(help);
                await navigationService.NavigateAsync(new Uri($"{NavigationConstants.HelpersMainPage}?selectedTab=CommitmentsPage", UriKind.Absolute));
            }
            else
            {
                await navigationService.GoBackAsync();
            }

        }
    }
}

