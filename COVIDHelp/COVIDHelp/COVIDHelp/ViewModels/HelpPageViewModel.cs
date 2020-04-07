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
using COVIDHelp.Helpers;
using System.Linq;

namespace COVIDHelp.ViewModels
{
    public class HelpPageViewModel : BaseViewModel
    {
        public ObservableCollection<Help> HelpsPerson { get; set; }

        private Help selectHelp;


        public DelegateCommand GoToDetailCommand { get; set; }
        public Help SelectHelp
        {
            get { 
                return selectHelp;  }
            set
            {
                selectHelp = value;
                if (selectHelp != null)
                {
                    GoToDetailCommand = new DelegateCommand(async () =>
                    {
                        await NavigateTo(SelectHelp);
                    });
                    GoToDetailCommand.Execute();
                }
            }
        }

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
            if (getrequest != null)
            {
                HelpsPerson = new ObservableCollection<Help>(getrequest);
            }
        }
        async Task NavigateTo(Help help)
        {
            var param = new NavigationParameters();
            param.Add("Helper", help);
            await navigationService.NavigateAsync(new Uri($"{NavigationConstants.RequestDetailPage}", UriKind.Relative),param);
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadPins = new DelegateCommand(async () => await GetPerson());
            LoadPins.Execute();

        }
    }
}
