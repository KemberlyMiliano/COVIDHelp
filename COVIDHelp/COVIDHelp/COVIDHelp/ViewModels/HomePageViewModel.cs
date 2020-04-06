using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class HomePageViewModel: BaseViewModel,INavigatedAware
    {
        public DelegateCommand<string>GoToMaps { get; set; }
        public DelegateCommand GoToMapsHelpers { get; set; }
        public bool IsVoluntary { get; set; }
        public User User { get; set; }
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            var param = new NavigationParameters();
            GoToMaps = new DelegateCommand<string>( async(filtrar)=> {
                
                Place place = new Place();
                place.Radius = 20000;
                place.TypePlace = filtrar;
                string jamon = $"{User.Latitude},{User.Longitude}";
                param.Add($"{nameof(Locations)}", jamon);
                param.Add($"{nameof(Place)}",place);
                await navigationService.NavigateAsync(new Uri(NavigationConstants.MapsPage,UriKind.Relative),param);
            });
            GoToMapsHelpers = new DelegateCommand(async () =>
            {
                param.Add($"{nameof(User)}", User);
                await navigationService.NavigateAsync(new Uri(NavigationConstants.MapsPage,UriKind.Relative), param);
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
    }
}
