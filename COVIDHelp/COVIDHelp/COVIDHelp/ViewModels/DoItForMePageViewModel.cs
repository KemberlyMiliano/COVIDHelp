using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class DoItForMePageViewModel : BaseViewModel
    {
        public ObservableCollection<Help> Requests { get; set; } = new ObservableCollection<Help>();
        public DoItForMePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {

        }
    }
}
