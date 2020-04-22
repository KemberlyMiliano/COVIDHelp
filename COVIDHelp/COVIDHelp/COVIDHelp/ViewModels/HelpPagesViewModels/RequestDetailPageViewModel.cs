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
    public class RequestDetailPageViewModel : BaseViewModel, IInitialize
    {
        public Help Help { get; set; }
        public DelegateCommand GoToHistorial { get; set; }
        public DelegateCommand GoToMaps { get; set; }
        public DelegateCommand CallNeededCommand { get; set; }
        public RequestDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            var param = new NavigationParameters();

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

            CallNeededCommand = new DelegateCommand(() =>
            {
                Call(Help.Telefono);
            });
        }
        public void Call(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (Exception ex)
            {
            }
        }
        public async Task DisplayAction()
        {
            bool action = await dialogService.DisplayAlertAsync("", "Estás seguro/a?", "Aceptar", "Cancelar");
            Int64 cedula = 0;
            string status = "";
            var id = cedula.GetPreferencesInt("Cedula");
            var user = await apiCovitServices.FindUser(id);
            if (action && user != null)
            {
                Help help = new Help
                {
                    Id = Help.Id,
                    NombreVoluntario = $"{user.Nombres} {user.Apellidos}",
                    CedulaVoluntario = user.Cedula,
                    TelefonoVoluntario = user.Telefono,
                    Status = $"{EState.Proceso}",
                    EmailVoluntario = user.Correo,
                    PosicionVoluntario = $"{user.Latitude}{user.Longitude}",
                    FechaEnviado = DateTime.Now

                };
                await apiCovitServices.PutHelp(help);
                await navigationService.NavigateAsync(new Uri($"{NavigationConstants.NavigationPage}{NavigationConstants.HelpersMainPage}?selectedTab={NavigationConstants.CommitmentsPage}", UriKind.Absolute));
            }
            else
            {
                await navigationService.GoBackAsync();
            }
        }

        public void Initialize(INavigationParameters parameters)
        {
            var param = parameters[Constants.TypeHelp] as Help;
            Help = param;
        }
    }
}

