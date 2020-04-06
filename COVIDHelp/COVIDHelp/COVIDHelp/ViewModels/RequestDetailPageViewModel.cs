using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class RequestDetailPageViewModel: BaseViewModel
    {
        public DelegateCommand GoToHistorial { get; set; }
        public DelegateCommand GoBack { get; set; }
        public RequestDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            var param = new NavigationParameters();

            GoBack = new DelegateCommand(async () =>
            {
                await navigationService.GoBackAsync();
            });

            GoToHistorial = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.CommitmentsPage, UriKind.Relative), param);
            });
        }
    }
}
