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
        public ObservableCollection<Help> HelpsPerson { get; set; }
        public DelegateCommand LoadPins { get; set; }
        public DelegateCommand<Necesity> GoToRequestDetail { get; set; }
        IApiGoogleServices apiGoogleServices;
        public HelpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
            this.apiGoogleServices = apiGoogleServices;
        }
        public async Task GetPerson()
        {
            var getrequest = await apiCovitServices.GetHelpActive();
            if (getrequest!=null)
            {
                HelpsPerson = new ObservableCollection<Help>(getrequest);
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
