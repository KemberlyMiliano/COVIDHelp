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

namespace COVIDHelp.ViewModels
{
    public class CommitmentsPageViewModel : BaseViewModel
    {
        public DelegateCommand<Help> ContactCommand { get; set; }
        public DelegateCommand<Help> DetailCommand { get; set; }
        public DelegateCommand LoadHisotiral { get; set; }
        public ObservableCollection<Help> Historial { get; set; }
        public DelegateCommand RefreshCommand { get; set; }
        public bool IsRefresh { get; set; }
        public CommitmentsPageViewModel(INavigationService navigationService, IPageDialogService dialogService,ICovidUserServices userServices ,IHelpServices helpServices) : base(navigationService, dialogService, userServices, helpServices)
        {
            LoadHisotiral = new DelegateCommand(async () =>
            {
                await GetHistorialHelper();
            });
            LoadHisotiral.Execute();
            ContactCommand = new DelegateCommand<Help>(async (help) =>
            {
                var user  = await userServices.FindUser("Id", help.UserID, Setting.Token);
                await OpenWhatsApp($"{user.Phone}", "Hola! Estoy aquí para ayudarte");

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
            var param = new NavigationParameters
            {
                { "Helper", help }
            };
            await navigationService.NavigateAsync(new Uri($"{NavigationConstants.NecesityDetailPage}", UriKind.Relative), param);
        }
        async Task GetHistorialHelper()
        {
            var request = await helpServices.GetHelp("All","All",Setting.Token);
            if (request != null)
            {
                Historial = new ObservableCollection<Help>(request.Where(e => e.UserID == Setting.Id));
            }

        }
        async Task OpenWhatsApp(string number, string text)
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
