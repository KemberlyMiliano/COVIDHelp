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
        public DelegateCommand<string>GoToAssistence { get; set; }
        public MedicalAssistenceRequestPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            GoToAssistence = new DelegateCommand<string>(async (param) =>
            {
                await navigationService.NavigateAsync(new Uri(param, UriKind.Relative));

            });

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
