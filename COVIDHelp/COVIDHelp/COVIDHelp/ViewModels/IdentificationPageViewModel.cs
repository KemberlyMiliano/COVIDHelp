using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class IdentificationPageViewModel: BaseViewModel
    {
        public DelegateCommand GoToVolunteer { get; set; }
        public IdentificationPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            GoToVolunteer = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.HelpPage, UriKind.Relative));
            });
        }
    }
}
