using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels.HelpersViewModels
{
    public class HelpersHomePageViewModel : BaseViewModel
    {
        public DelegateCommand GoToHelpPage { get; set; }
        public HelpersHomePageViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {
            GoToHelpPage = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync("HelpPage");
            });


        }
    }
}
