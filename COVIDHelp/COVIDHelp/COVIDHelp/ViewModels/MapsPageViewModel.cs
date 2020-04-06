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
    public class MapsPageViewModel: BaseViewModel, INavigationAware
    {
        public ObservableCollection<Place> PlaceNearbys { get; set; }
        public ObservableCollection<User> NeaderPerson { get; set; }

        public DelegateCommand LoadPins { get; set; }
        IApiGoogleServices apiGoogleServices;
        public MapsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
            this.apiGoogleServices = apiGoogleServices;
        }
        async Task GetPlace(Locations locations,int radius, string type)
        {
            var getresquest = await apiGoogleServices.GetNearbyPlaces(ConfigApi.ApiKeyGoogle, locations,radius,type);
            var places = getresquest.Results;
            if (places != null&&places.Count>0)
            {
                PlaceNearbys = new ObservableCollection<Place>(places);
            }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var param = parameters[$"{nameof(Place)}"] as Place;
            var param2 = parameters[$"{nameof(Locations)}"] as Locations;
            LoadPins = new DelegateCommand(async () => await GetPlace(param2, param.Radius, param.TypePlace));
            LoadPins.Execute();

        }
    }
}
