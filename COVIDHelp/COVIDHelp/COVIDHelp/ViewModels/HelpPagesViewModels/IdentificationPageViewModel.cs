using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using COVIDHelp.Helpers;

namespace COVIDHelp.ViewModels
{
    public class IdentificationPageViewModel : BaseViewModel
    {
        public DelegateCommand<string> GoToVolunteer { get; set; }
        public IdentificationPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices, IHelpServices helpServices) : base(navigationService, dialogService, userServices,helpServices)
        {
            GoToVolunteer = new DelegateCommand<string>(async (filter) =>
            {

                var param = new NavigationParameters
                {
                    { Constants.TypeHelp, filter }
                };
                await navigationService.NavigateAsync(new Uri(NavigationConstants.HelpPage, UriKind.Relative), param);
            });
        }
    }
}
