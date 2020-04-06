using COVIDHelp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationService navigationService;
        protected IPageDialogService dialogService;
        protected IApiCovitServices apiCovitServices;
        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.apiCovitServices = apiCovitServices;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
