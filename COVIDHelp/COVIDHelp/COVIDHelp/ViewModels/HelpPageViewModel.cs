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
    public class HelpPageViewModel : BaseViewModel
    {
        public ObservableCollection<Help> HelpsPerson { get; set; }
        public List<Pin> Pins { get; set; }
        public DelegateCommand GoToDetailCommand { get; set; }
        public DelegateCommand LoadPins { get; set; }
        public DelegateCommand<Help> GoToDetail { get; set; }
        public bool IsReshing { get; set; }
        IApiGoogleServices apiGoogleServices;
        public HelpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
            this.apiGoogleServices = apiGoogleServices;

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
        }
        public async Task GetPerson()
        {
            string status = "";

            if (status.GetPreferences("Who") == "Medicamentos")
            {
                var emergencia = await apiCovitServices.GetHelpActive("Emergencia");
                HelpsPerson = new ObservableCollection<Help>(emergencia);
                var asistencia = await apiCovitServices.GetHelpActive("Asistencia");

                foreach (var item in asistencia)
                {
                    HelpsPerson.Add(item);
                }

                var psicologica = await apiCovitServices.GetHelpActive("Psicologica");

                foreach (var item in psicologica)
                {
                    HelpsPerson.Add(item);
                }
            }

            else
            {
                var alimentos = await apiCovitServices.GetHelpActive("Alimentos");
                HelpsPerson = new ObservableCollection<Help>(alimentos);
                var medicamentos = await apiCovitServices.GetHelpActive("Medicamentos");
                foreach (var item in medicamentos)
                {
                    HelpsPerson.Add(item);
                }
            }

        }
        async Task NavigateTo(Help help)
        {
            var param = new NavigationParameters();
            param.Add("Helper", help);
            await navigationService.NavigateAsync(new Uri($"{NavigationConstants.RequestDetailPage}", UriKind.Relative), param);
        }
        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }
        public void OnNavigatedTo(INavigationParameters parameters)
        {
            LoadPins = new DelegateCommand(async () => await GetPerson());
            LoadPins.Execute();
        }
    }
}
