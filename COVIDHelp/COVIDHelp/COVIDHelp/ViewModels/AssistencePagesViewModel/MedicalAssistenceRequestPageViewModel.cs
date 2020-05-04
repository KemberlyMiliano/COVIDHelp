using COVIDHelp.Helpers;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class MedicalAssistenceRequestPageViewModel : BaseViewModel, INavigationAware
    {
        public DelegateCommand<string> GoToAssistence { get; set; }
        public MedicalAssistenceRequestPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            GoToAssistence = new DelegateCommand<string>(async (type) =>
            {
                var param = new NavigationParameters
                {
                    { Constants.TypeHelp, type }
                };
                await navigationService.NavigateAsync(new Uri(NavigationConstants.SelectAssistence, UriKind.Relative),param);

            });
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {

        }
    }
}
