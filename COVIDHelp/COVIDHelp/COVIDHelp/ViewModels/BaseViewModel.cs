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
        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
        }    
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
