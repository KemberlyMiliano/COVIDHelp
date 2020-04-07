using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace COVIDHelp.ViewModels.LoginAndRegisterViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public DelegateCommand GoToVolunteer { get; set; }
        public DelegateCommand GoToMaps { get; set; }
        public DelegateCommand GoToMedicalAttention { get; set; }
        public DelegateCommand GoToProfile { get; set; }
        public DelegateCommand EmergencyCommand { get; set; }
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            GoToVolunteer = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.HelpPage);
            });

            GoToMaps = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.MapsPage);
            });
            GoToMedicalAttention = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.HelpPage);
            });

            GoToProfile = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.ProfilePage);
            });

            EmergencyCommand = new DelegateCommand( () =>
            {
                PlacePhoneCall("911");
            });
        }
        public void PlacePhoneCall(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }

            catch (Exception ex)
            {
                ProcessException(ex);
            }
        }

        private async void ProcessException(Exception ex)
        {
            if (ex != null)
                await dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
        }
    }

}
