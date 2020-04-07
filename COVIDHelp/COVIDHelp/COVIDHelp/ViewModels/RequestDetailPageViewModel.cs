using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels
{
    public class RequestDetailPageViewModel : BaseViewModel
    {
        public Help Help { get; set; }
        public DelegateCommand GoToHistorial { get; set; }
        public DelegateCommand GoBack { get; set; }
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

        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //Recibir la ayuda de la lista de HelpPage a esta page
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            //var param = parameters["Helper"] as Help;
            //Help = param;
            //Enviar el objeto que se recibio de HelpPage agregandolo la lista de historial

        }
        public async Task DisplayAction()
        {
            bool action = await dialogService.DisplayAlertAsync("", "Estás seguro/a?", "Aceptar", "Cancelar");

            if (action)
            {
                // Agregar al historial del usuario
                await navigationService.NavigateAsync(new Uri(NavigationConstants.CommitmentsPage, UriKind.Relative));
            }

            await navigationService.GoBackAsync();
        }
    }
}

