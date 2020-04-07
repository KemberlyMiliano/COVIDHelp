using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;

namespace COVIDHelp.ViewModels
{
    public class HelpPageViewModel : BaseViewModel, INavigationAware
    {
        public ObservableCollection<User> NeaderPerson { get; set; }
        public List<Pin> NeederPin { get; set; }
        public ObservableCollection<Necesity> Requests { get; set; } = new ObservableCollection<Necesity>();
        public DelegateCommand LoadPins { get; set; }
        public DelegateCommand<Necesity> GoToRequestDetail { get; set; }
        public HelpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Requests.Add(new Necesity { Image = "defaultUser", NeededPerson = "Eladio Rodriguez", Status = "Regular" });
            Requests.Add(new Necesity { Image = "defaultUser", NeededPerson = "Eladio Rodriguez", Status = "Regular" });
            Requests.Add(new Necesity { Image = "defaultUser", NeededPerson = "Eladio Rodriguez", Status = "Regular" });
            Requests.Add(new Necesity { Image = "defaultUser", NeededPerson = "Eladio Rodriguez", Status = "Regular" });
            Requests.Add(new Necesity { Image = "defaultUser", NeededPerson = "Eladio Rodriguez", Status = "Regular" });

            GoToRequestDetail = new DelegateCommand<Necesity>(async (necesity) =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.RequestDetailPage, UriKind.Relative));
            });
        }
        async Task GetPerson()
        {
            var getrequest = await apiCovitServices.GetUser();
            var users = getrequest;
            if (users != null && users.Count > 0)
            {
                NeaderPerson = new ObservableCollection<User>(users);
            }
        }
        async Task GetPins()
        {
            var getrequest = await apiCovitServices.GetUser();
            var users = getrequest;
            if (users != null && users.Count > 0)
            {
                NeaderPerson = new ObservableCollection<User>(users);
            }
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var param = parameters[$"{nameof(User)}"] as User;

            LoadPins = new DelegateCommand(async () => await GetPerson());
            LoadPins.Execute();

        }
    }
}
