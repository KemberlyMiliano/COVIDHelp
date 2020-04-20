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
    public class EditProfilePageViewModel : BaseViewModel
    {
        public User User { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand LoadUserCommand { get; set; }
        public EditProfilePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Int64 cedula = 0;
            var id = cedula.GetPreferencesInt("Cedula");
            LoadUserCommand = new DelegateCommand(async () =>
            {
                await GetUser(id);
            });

            LoadUserCommand.Execute();
            EditCommand = new DelegateCommand(async () =>
            {
                var param = new NavigationParameters();
                param.Add("EditUser", User);
                await navigationService.GoBackAsync();
            });
        }
        async Task GetUser(Int64 cedula)
        {
            var user = await apiCovitServices.FindUser(cedula);
            User = user;

        }
    }

}
