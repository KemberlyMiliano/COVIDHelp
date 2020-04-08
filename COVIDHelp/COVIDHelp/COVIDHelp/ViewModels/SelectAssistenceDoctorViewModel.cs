using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using MonkeyCache.FileStore;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace COVIDHelp.ViewModels
{
    public class SelectAssistenceDoctorViewModel : SelectAssistanceBaseViewModel
    {
        public SelectAssistenceDoctorViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Diseases = new ObservableCollection<Diseases>()
            {
                new Diseases { Name = "Fiebre" },
                new Diseases { Name = "Dolor de cabeza" },
                new Diseases { Name = "Dificultad para respirar" },
            };

            AddDataAndNavigateCommand = new DelegateCommand<string>(async (param) =>
            {
                await DisplayAction(param);
            });
        }
    }
}
