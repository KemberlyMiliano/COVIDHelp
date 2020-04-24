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

namespace COVIDHelp.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel, IInitialize
    {
        public User User { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand LoadProfile { get; set; }
        public ProfilePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            LoadProfile = new DelegateCommand(async () => await FindUser());
            LoadProfile.Execute();
            EditCommand = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.EditProfilePage, UriKind.Relative));

            });
           

        }


        async Task FindUser()
        {
            Int64 cedula = 0;
            var user = await apiCovitServices.FindUser(cedula.GetPreferencesInt("Cedula"));
            if (user != null)
            {
                User = user;
            }
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey(Constants.PersonKey))
            {
                var param = parameters[Constants.PersonKey] as User;
                User = param;
            }
        }

    }
}
