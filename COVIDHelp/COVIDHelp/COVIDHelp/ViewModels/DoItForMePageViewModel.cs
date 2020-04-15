using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels
{
    public class DoItForMePageViewModel : BaseViewModel
    {
        public ObservableCollection<Request> Requests { get; set; } = new ObservableCollection<Request>();
        public DelegateCommand AddToGlobalRequests { get; set; }
        public string Text { get; set; }
        public DelegateCommand<Request> OnDeleteCommand { get; set; }
        public DelegateCommand AddToList { get; set; }
        public DoItForMePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            AddToGlobalRequests = new DelegateCommand(async () =>
            {
                await DisplayAction();
            });

            AddToList = new DelegateCommand(() =>
            {
                Requests.Add(new Request
                {
                    Text = Text
                });
            });

            OnDeleteCommand = new DelegateCommand<Request>((param) =>
            {
                Requests.Remove(param);
            });

        }
        public async Task DisplayAction()
        {
            bool action = await dialogService.DisplayAlertAsync("", "Estás seguro/a?", "Aceptar", "Cancelar");

            if (action)
            {
                Int64 cedula = 0;
                string status = "";
                var user = await apiCovitServices.FindUser(cedula.GetPreferencesInt("Cedula"));
                if (user != null)
                {
                    var help = new Help
                    {
                        Nombre = $"{user.Nombres} {user.Apellidos}",
                        Cedula = user.Cedula,
                        Email = user.Correo,
                        Telefono = user.Telefono,
                        Posicion = $"{user.Latitude},{user.Longitude}",
                        Dirrecion = user.Direccion,
                        Status = "Activo",
                        FechaEnviado = DateTime.Now,
                        Tipo = status.GetPreferences("status")

                    };

                    foreach (var item in Requests)
                    {
                        help.DescripcionProblema += $"{item.Text}\n";

                    }
                    var probar = await apiCovitServices.PostHelp(help);
                    await navigationService.GoBackToRootAsync();
                }
            }
        }
    }
}
