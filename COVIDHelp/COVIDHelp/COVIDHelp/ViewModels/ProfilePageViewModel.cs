using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using COVIDHelp.Helpers;
using Prism.Commands;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;
using System.Linq;
using Prism.Services.Dialogs;

namespace COVIDHelp.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel, IInitialize
    {
        public User User { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand LoadProfile { get; set; }
        public DelegateCommand LogOutCommand { get; set; }
        public ProfilePageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices, IHelpServices helpServices, IDialogService dialog) : base(navigationService, dialogService, userServices,helpServices)
        {
            LoadProfile = new DelegateCommand(async () => await FindUser());
            LoadProfile.Execute();
            EditCommand = new DelegateCommand(() =>
            {
                var param = new DialogParameters { { Constants.PasswordKey,"Password"}, { Constants.PersonKey, User } };
                dialog.ShowDialog("DialogEntry", param, CloseDialogCallback);

            });
            LogOutCommand = new DelegateCommand(async () =>
            {
                Setting.CloseLogged();
                await navigationService.NavigateAsync(new Uri(NavigationConstants.LoginPage,UriKind.Absolute));
            });

        }
       async void CloseDialogCallback(IDialogResult dialogResult)
        {
            if (dialogResult.Parameters.ContainsKey(Constants.PersonKey))
            {
                User = dialogResult.Parameters.GetValue<User>(Constants.PersonKey);
                var param = new NavigationParameters
                {
                    { Constants.PersonKey, User }
                };
                await navigationService.NavigateAsync(new Uri(NavigationConstants.EditProfilePage, UriKind.Relative), param);
            }
        }
        async Task FindUser()
        {
            var user = await userServices.FindUser(Constants.IdKey, Setting.Id, Setting.Token);
            if (user != null)
            {
                User = user;
            }
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(Constants.PhoneKey))
            {
                var param = parameters[Constants.PersonKey] as User;
                User = param;
            }
        }

    }
}
