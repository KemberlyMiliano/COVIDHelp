using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels.DialogServices
{
    public class DialogRateViewModel:BaseViewModel,IDialogAware
    {
        public User User { get; set; }
        public DelegateCommand RateCommand { get; set; }
        public DialogRateViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            RateCommand = new DelegateCommand(async () =>
            {
                await apiCovitServices.EvaluateVoluntary(User, Setting.Token);
                RequestClose(null);
            });
        }

        public event Action<IDialogParameters> RequestClose;

        public bool CanCloseDialog()
        {
            if (User != null)
                return User.Raters != 0 ? true : false;
            else
                return true;
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            var voluntary = parameters.GetValue<User>(Constants.IdKey);
            if (voluntary!=null)
            {
                User = voluntary;
            }
            else
            {
                RequestClose(null);
            }
        }
    }
}
