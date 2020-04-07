using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class NecesityDetailPageViewModel : BaseViewModel
    {
        public DelegateCommand GoBackCommand { get; set; }
        public NecesityDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            GoBackCommand = new DelegateCommand(async () =>
            {
                await navigationService.GoBackAsync();

            });
        }
    }
}
