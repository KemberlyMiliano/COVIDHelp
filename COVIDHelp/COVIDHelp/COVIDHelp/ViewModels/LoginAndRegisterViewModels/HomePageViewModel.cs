using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels.LoginAndRegisterViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        public DelegateCommand GoToVolunteer { get; set; }
        public DelegateCommand GoToMaps { get; set; }
        public DelegateCommand GoToMedicalAttention { get; set; }
        public DelegateCommand GoToProfile { get; set; }
        public HomePageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            GoToVolunteer = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.HelpPage);
            });

            GoToMaps = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.MapsPage);
            });
            GoToMedicalAttention = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.HelpPage);
            });

            GoToProfile = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(NavigationConstants.ProfilePage);
            });
        }
    }

}
