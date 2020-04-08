using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using COVIDHelp.Helpers;

namespace COVIDHelp.ViewModels
{
    public class IdentificationPageViewModel: BaseViewModel,INavigatedAware
    {
        public DelegateCommand<string> GoToVolunteer { get; set; }
        public User User { get; set; }
        public IdentificationPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Int64 cedula = 0;
            apiCovitServices.FindUser(cedula.GetPreferencesInt("Cedula"));
            GoToVolunteer = new DelegateCommand<string>(async (filter) =>
            {
                string IsProfesional = filter == "Medicamentos" ? "Medicamentos" : "Alimentos";
                IsProfesional.SaveString("Who");
                
                var param = new NavigationParameters
                {
                    { "User", User }
                };
                await navigationService.NavigateAsync(new Uri(NavigationConstants.HelpPage, UriKind.Relative),param);
            });
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
           
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var param = parameters["User"] as User;
            User = param;
        }
    }
}
