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
using Prism.Navigation.Xaml;

namespace COVIDHelp.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel, IInitialize
    {
        public User User { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand LoadProfile { get; set; }
        public DelegateCommand LogOutCommand { get; set; }
        public ProfilePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            LoadProfile = new DelegateCommand(async () => await FindUser());
            LoadProfile.Execute();
            EditCommand = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.EditProfilePage, UriKind.Relative));

            });
            LogOutCommand = new DelegateCommand(async () =>
            {
                Setting.CloseLogged();
                await navigationService.NavigateAsync(new Uri(NavigationConstants.LoginPage,UriKind.Absolute));
            });



        }


        async Task FindUser()
        {
            var user = await apiCovitServices.FindUser(Constants.IdKey, Setting.Id, Setting.Token);
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
