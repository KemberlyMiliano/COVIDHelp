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

namespace COVIDHelp.ViewModels
{
    public class ProfilePageViewModel: BaseViewModel
    {
        public User User { get; set; }
        public DelegateCommand LoadProfile { get; set; }
        public ProfilePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            LoadProfile = new DelegateCommand(async () => await FindUser());
            LoadProfile.Execute();
        }
        async Task FindUser() {
            Int64 cedula = 0;
          var user =  await apiCovitServices.FindUser(cedula.GetPreferencesInt("Cedula"));
            if (user!=null)
            {
                User = user;
            }
        }

    }
}
