using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Prism.Services.Dialogs;
using Prism.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels.DialogServices
{
    public class DialogEntryViewModel:BaseViewModel, IDialogAware
    {
        public User User { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand CloseCommad { get => new DelegateCommand(() => canClose = true); }
        public DialogEntryViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices, IHelpServices helpServices,IAuthenticationService authenticationService) : base(navigationService, dialogService, userServices, helpServices)
        {
            SaveCommand = new DelegateCommand(async () =>
            {
                switch (Title)
                {
                    case "Password":
                        User.Password = Value;
                        var user = await authenticationService.LoginUser(User);
                        canClose = user.IsSuccessStatusCode;
                        var param = new DialogParameters { {  Constants.PersonKey, User }};
                        RequestClose(param);
                        break;
                    case "Code":
                        await userServices.ValidateCode(User.Phone, Convert.ToInt32(Value), Setting.Token);
                        canClose = true;
                        RequestClose(null);
                        break;
                    default:
                        canClose = false;
                        break;
                }
                
            });
        }

        public event Action<IDialogParameters> RequestClose;
        private bool canClose;
        public  bool CanCloseDialog()
        {
            return canClose;
        }

        public void OnDialogClosed()
        {
            throw new NotImplementedException();
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey(Constants.PasswordKey))
            {
                Title = parameters.GetValue<string>(Constants.PasswordKey);
                User = parameters.GetValue<User>(Constants.PersonKey);
            }
            else
            {

            }
        }
    }
}
