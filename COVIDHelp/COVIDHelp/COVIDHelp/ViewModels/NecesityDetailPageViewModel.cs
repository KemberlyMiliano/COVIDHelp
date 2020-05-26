using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels
{
    public class NecesityDetailPageViewModel : BaseViewModel, INavigatedAware
    {
        public Help Help { get; set; }
        public NecesityDetailPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices,IHelpServices helpServices) : base(navigationService, dialogService, userServices,helpServices)
        {
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var param = parameters["Helper"] as Help;
            Help = param;
        }
    }
}
