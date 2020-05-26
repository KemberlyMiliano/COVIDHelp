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
using Xamarin.Forms.OpenWhatsApp;

namespace COVIDHelp.ViewModels
{
    public class RequestDetailPageViewModel : BaseViewModel, IInitialize
    {
        public Help Help { get; set; }
        public DelegateCommand GoToHistorial { get; set; }
        public DelegateCommand GoToMaps { get; set; }
        public DelegateCommand CallNeededCommand { get; set; }
        public DelegateCommand ContactCommand { get; set; }
        public RequestDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices,IHelpServices helpServices) : base(navigationService, dialogService, userServices,helpServices)
        {
            var param = new NavigationParameters();

            GoToHistorial = new DelegateCommand(async () =>
            {
                await DisplayAction();
            });

            ContactCommand = new DelegateCommand(async () =>
            {
                var user = await userServices.FindUser("Id", Help.UserID, Setting.Token);
                await OpenWhatsApp($"{user.Phone}", "Hola! Estoy aquí para ayudarte");

            });

            GoToMaps = new DelegateCommand(async () =>
            {
                double lat = Convert.ToDouble( Help.Latitude);
                double lng = Convert.ToDouble(Help.Longitude);
                await Map.OpenAsync(lat, lng, new MapLaunchOptions
                {
                    Name = Help.Needed.Name,
                    NavigationMode = Xamarin.Essentials.NavigationMode.Driving
                });
            });

            CallNeededCommand = new DelegateCommand(() =>
            {
                Call($"{Help.Needed.Phone}");
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
        public async Task DisplayAction()
        {
            bool action = await dialogService.DisplayAlertAsync("", "Estás seguro/a?", "Aceptar", "Cancelar");
            string status = "";
            var user = await userServices.FindUser(Constants.IdKey, Setting.Id, Setting.Token);
            if (action && user != null)
            {
                Help help = new Help
                {
                    Id = Help.Id,
                    VolunteerID=user.Id,
                    Status = $"{EState.Proceso}",

                };
                await helpServices.PutHelp(help,Setting.Token);
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

