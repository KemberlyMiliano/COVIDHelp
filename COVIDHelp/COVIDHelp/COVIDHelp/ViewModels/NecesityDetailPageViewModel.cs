using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class NecesityDetailPageViewModel : BaseViewModel,INavigatedAware
    {
        public Help Help { get; set; }
        public DelegateCommand GoBackCommand { get; set; }
        public NecesityDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            GoBackCommand = new DelegateCommand(async () =>
            {

            });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var param = parameters["Helper"] as Help;
            Help = param;
        }
    }
}
