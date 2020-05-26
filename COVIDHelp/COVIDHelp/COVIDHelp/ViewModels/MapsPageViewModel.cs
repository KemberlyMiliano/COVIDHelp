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
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.ViewModels
{
    public class MapsPageViewModel : BaseViewModel, INavigationAware
    {
        public Place Place { get; set; }
        private Pin selectPlace;
        public Pin SelectPlace
        {
            get { return selectPlace; }
            set
            {
                selectPlace = value;
                if (selectPlace != null)
                {
                    Place = PlaceNearbys.Find(e => e.Vicinity == selectPlace.Address);
                    IsVisible = true;
                }
            }
        }
        public bool IsVisible { get; set; } = false;
        private string status;
        public DelegateCommand GoToDoItForMe { get; set; }
        public List<Place> PlaceNearbys { get; set; }
        public DelegateCommand LoadPins { get=> new DelegateCommand(async () =>
        {
            var param = new NavigationParameters();
            await navigationService.NavigateAsync(new Uri(NavigationConstants.DoItForMePage, UriKind.Relative), param);
        }); }

        readonly IApiGoogleServices apiGoogleServices;
        public MapsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices,IHelpServices helpServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, userServices,helpServices)
        {
            this.apiGoogleServices = apiGoogleServices;
     

            GoToDoItForMe = new DelegateCommand(async () =>
            {
                var param = new NavigationParameters();
                await navigationService.NavigateAsync(new Uri(NavigationConstants.DoItForMePage, UriKind.Relative), param);
            });
        }

        public async Task GetPlace(string locations, int radius, string type)
        {
            var getresquest = await apiGoogleServices.GetNearbyPlaces(ConfigApi.ApiKeyGoogle, locations, radius, type);
            var places = getresquest.Results;
            if (places != null)
            {
                PlaceNearbys = new List<Place>(places);
            }
        }
       
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }
    }
}
