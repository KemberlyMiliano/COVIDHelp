using COVIDHelp.Helpers;
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

namespace COVIDHelp.ViewModels
{
    public class RequestsListPageViewModel : BaseViewModel
    {
        public DelegateCommand<Help> ContactCommand { get; set; }
        public DelegateCommand<Help> DetailCommand { get; set; }
        public DelegateCommand LoadHisotiral { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public bool IsRefresh { get; set; }
        public ObservableCollection<Help> Requests { get; set; }
        public RequestsListPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            LoadHisotiral = new DelegateCommand(async () =>
            {
                await GetHistorialHelper();
            });

            LoadHisotiral.Execute();

            ContactCommand = new DelegateCommand<Help>(async (help) =>
            {
                await OpenWhatsApp(help.Telefono, "Hola! Estoy aquí para ayudarte");
            });

            RefreshCommand = new DelegateCommand(async () =>
            {
                IsRefresh = true;
                await GetHistorialHelper();
                IsRefresh = false;
            });

            DetailCommand = new DelegateCommand<Help>(async (param) =>
            {
                await NavigateTo(param);
            });
        }
        async Task NavigateTo(Help help)
        {
            var param = new NavigationParameters();
            param.Add("Helper", help);
            await navigationService.NavigateAsync(new Uri($"{NavigationConstants.NecesityDetailPage}", UriKind.Relative), param);
        }
        async Task GetHistorialHelper()
        {
            var request = await apiCovitServices.GetHelp();
            Int64 cedula = 0;
            if (request != null)
            {
                Requests = new ObservableCollection<Help>(request.Where(e => e.Cedula == cedula.GetPreferencesInt("Cedula")));
            }
        }
        private async Task OpenWhatsApp(string number, string text)
        {
            try
            {
                Chat.Open($"+1{number}", text);
            }

            catch (Exception ex)
            {
                await dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
    }
}
