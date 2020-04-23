using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels.LoginAndRegisterViewModels
{
    public class EditProfilePageViewModel : BaseViewModel,IInitialize
    {
        public User User { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public EditProfilePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            EditCommand = new DelegateCommand(async () =>
            {
                User = await apiCovitServices.UpdateUser(User);
                var param = new NavigationParameters
                {
                    { Constants.PersonKey, User }
                };

                await navigationService.GoBackAsync();
            });
        }

        public void Initialize(INavigationParameters parameters)
        {
            var user = parameters[Constants.PersonKey] as User;
            User = user;
        }
    }

}
