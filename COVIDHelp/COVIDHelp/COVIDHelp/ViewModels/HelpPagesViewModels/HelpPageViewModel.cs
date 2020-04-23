using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.GoogleMaps;
using COVIDHelp.Helpers;
using System.Linq;

namespace COVIDHelp.ViewModels
{
    public class HelpPageViewModel : BaseViewModel,IInitialize
    {
        public ObservableCollection<Help> HelpsPerson { get; set; }
        public List<Pin> Pins { get; set; }
        public DelegateCommand GoToDetailCommand { get; set; }
        public DelegateCommand LoadPins { get; set; }
        public DelegateCommand<Help> GoToDetail { get; set; }
        public bool IsReshing { get; set; }
        private string typeHelp;
        public HelpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices ) : base(navigationService, dialogService, apiCovitServices)
        {

            GoToDetailCommand = new DelegateCommand(async () =>
            {
                IsReshing = true;
                await GetPerson();
                IsReshing = false;
            });

            GoToDetailCommand.Execute();

            GoToDetail = new DelegateCommand<Help>(async (param) =>
            {
                await NavigateTo(param);
            });
            LoadPins = new DelegateCommand(async () => await GetPerson());

        }
        public async Task GetPerson()
        {
            HelpsPerson = new ObservableCollection<Help>();
            if (typeHelp == $"{ETypeHelp.Medicamentos}")
            {
                var emergencia = await apiCovitServices.GetHelpActive($"{ETypeHelp.Emergencias}");
                var asistencia = await apiCovitServices.GetHelpActive($"{ETypeHelp.Asistencia}");
                var psicologica = await apiCovitServices.GetHelpActive($"{ETypeHelp.Psicologo}");
                emergencia.AddRange(asistencia);
                emergencia.AddRange(psicologica);
                HelpsPerson = new ObservableCollection<Help>(emergencia);
            }
            else
            {
                var alimentos = await apiCovitServices.GetHelpActive($"{ETypeHelp.Medicamentos}");
                var medicamentos = await apiCovitServices.GetHelpActive($"{ETypeHelp.Alimentos}");
                medicamentos.AddRange(alimentos);
                HelpsPerson = new ObservableCollection<Help>(medicamentos);
            }

        }
        async Task NavigateTo(Help help)
        {
            var param = new NavigationParameters
            {
                { Constants.TypeHelp, help }
            };
            await navigationService.NavigateAsync(new Uri($"{NavigationConstants.NavigationPage}{NavigationConstants.RequestDetailPage}", UriKind.Relative), param);
        }

        public void Initialize(INavigationParameters parameters)
        {
            typeHelp = parameters[Constants.TypeHelp] as string;
            LoadPins.Execute();

        }
    }
}
