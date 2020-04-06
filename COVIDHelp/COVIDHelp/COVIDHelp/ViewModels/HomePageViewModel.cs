using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using COVIDHelp.Helpers;

using Xamarin.Essentials;


namespace COVIDHelp.ViewModels
{
    public class HomePageViewModel : BaseViewModel, INavigatedAware
    {
        public DelegateCommand<string> GoToMaps { get; set; }
        public DelegateCommand EmergencyCommand { get; set; }
        public DelegateCommand GoToMedicalAssintence { get; set; }
        public DelegateCommand GoToVolunteer { get; set; }
        public bool IsVoluntary { get; set; }
        public User User { get; set; }
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            var param = new NavigationParameters();

            GoToMaps = new DelegateCommand<string>(async (filtrar) =>
            {

                filtrar.SaveString("type");
                string latitude = $"{User.Latitude}";
                string longitude = $"{User.Longitude}";
                latitude.SaveString("latitude");
                longitude.SaveString("longitude");
                await navigationService.NavigateAsync(new Uri(NavigationConstants.MapsPage, UriKind.Relative));
            });

                GoToVolunteer = new DelegateCommand(async () =>
                {
                    await navigationService.NavigateAsync(new Uri(NavigationConstants.HelpPage, UriKind.Relative), param);
                });

                GoToMedicalAssintence = new DelegateCommand(async () =>
                {
                    await navigationService.NavigateAsync(new Uri(NavigationConstants.MedicalAssistenceRequestPage, UriKind.Relative), param);
                });

                EmergencyCommand = new DelegateCommand(() =>
                {
                    PlacePhoneCall("911");
                });
            }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey($"{nameof(User)}"))
            {
                var param = parameters[$"{nameof(User)}"] as User;
                User = param;
            }

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

        async void ProcessException(Exception ex)
        {
            if (ex != null)
                await dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
        }

    }
}
