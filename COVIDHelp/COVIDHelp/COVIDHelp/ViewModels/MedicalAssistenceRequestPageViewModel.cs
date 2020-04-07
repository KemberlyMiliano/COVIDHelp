using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class MedicalAssistenceRequestPageViewModel : BaseViewModel, INavigationAware
    {
        public DelegateCommand GoToNurseAssistence { get; set; }
        public DelegateCommand GoToDoctorAssistence { get; set; }
        public DelegateCommand GoToPsycologyAssistence { get; set; }
        public MedicalAssistenceRequestPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            GoToNurseAssistence = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.SelectAssistenceNurse, UriKind.Relative));

            });
            GoToDoctorAssistence = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.SelectAssistenceDoctor, UriKind.Relative));

            });
            GoToPsycologyAssistence = new DelegateCommand(async () =>
            {
                await navigationService.NavigateAsync(new Uri(NavigationConstants.SelectAssistencePsycology, UriKind.Relative));

            });

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }
}
