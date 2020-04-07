using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.OpenWhatsApp;
using COVIDHelp.Helpers;

namespace COVIDHelp.ViewModels.LoginAndRegisterViewModels
{
    public class CommitmentsPageViewModel : BaseViewModel
    {
        public DelegateCommand ContactCommand { get; set; }
        public DelegateCommand DetailCommand { get; set; }
        public DelegateCommand LoadHisotiral { get; set; }
        public ObservableCollection<Help> Historial { get; set; }
        public CommitmentsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            LoadHisotiral = new DelegateCommand(async () =>
            {
                await GetHistorialHelper();
            });
            LoadHisotiral.Execute();
            ContactCommand = new DelegateCommand(async () =>
            {
                //Agregar numero del usuario
                await OpenWhatsApp("+1 829 465 8565", "Hola! Estoy aquí para ayudarte");

            });

            DetailCommand = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.NecesityDetailPage, UriKind.Relative));
            });
        }
        async Task GetHistorialHelper()
        {
            var request = await apiCovitServices.GetHelp();
            Int64 cedula = 0;
            if (request != null)
            {
                Historial = new ObservableCollection<Help>(request.Where(e => e.Cedula == cedula.GetPreferencesInt("Cedula")));
            }

        }
        private async Task OpenWhatsApp(string number, string text)
        {
            try
            {
                Chat.Open(number, text);
            }
            catch (Exception ex)
            {
                await dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
    }

}
