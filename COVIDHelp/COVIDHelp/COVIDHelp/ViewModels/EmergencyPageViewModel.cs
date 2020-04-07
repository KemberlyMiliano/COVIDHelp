using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class EmergencyPageViewModel : BaseViewModel, IDialogAware
    {
        public DelegateCommand CloseCommand { get; }
        public EmergencyPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {

            CloseCommand = new DelegateCommand(async () =>
            {
                RequestClose(null);
                await dialogService.DisplayAlertAsync("", "Su emergencia fue enviada!", "OK");
            });
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog() => true;

        public void OnDialogClosed()
        {

        }
        public void OnDialogOpened(IDialogParameters parameters)
        {

        }
    }
}
