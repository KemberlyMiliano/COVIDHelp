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

namespace COVIDHelp.ViewModels
{
    public class HelpPageViewModel : BaseViewModel, INavigationAware
    {
        public ObservableCollection<User> NeaderPerson { get; set; }

        public DelegateCommand LoadPins { get; set; }
        public HelpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
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
