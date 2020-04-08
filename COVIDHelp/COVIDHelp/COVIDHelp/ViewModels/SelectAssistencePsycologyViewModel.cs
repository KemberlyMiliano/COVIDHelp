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
    public class SelectAssistencePsycologyViewModel : SelectAssistanceBaseViewModel
    {
        public SelectAssistencePsycologyViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Diseases = new ObservableCollection<Diseases>()
            {
                new Diseases { Name = "Ansiedad" },
                new Diseases { Name = "Trastornos alimenticios" },
                new Diseases { Name = "Paranoia" },
            };

            AddDataAndNavigateCommand = new DelegateCommand<string>(async (param) =>
            {
                await DisplayAction(param);

            });
        }
    }
}
