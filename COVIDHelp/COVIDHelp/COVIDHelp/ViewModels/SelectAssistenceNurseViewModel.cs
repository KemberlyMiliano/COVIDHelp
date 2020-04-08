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
    public class SelectAssistenceNurseViewModel : SelectAssistanceBaseViewModel
    {
        public SelectAssistenceNurseViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Diseases = new ObservableCollection<Diseases>()
            {
                new Diseases { Name = "Hipertension" },
                new Diseases { Name = "Diabetes" },
                new Diseases { Name = "Presion Arterial" },
            };

            AddDataAndNavigateCommand = new DelegateCommand<string>(async (paran) =>
            {
                await DisplayAction(paran);
            });
        }

    }
}
