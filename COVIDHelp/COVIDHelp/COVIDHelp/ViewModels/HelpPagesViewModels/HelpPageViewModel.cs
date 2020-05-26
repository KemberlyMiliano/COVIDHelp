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
        public DelegateCommand GoToDetailCommand { get=> new DelegateCommand(async () =>
        {
            IsReshing = true;
            await GetPerson();
            IsReshing = false;
        }); }
        public DelegateCommand LoadPins { get; set; }
        public DelegateCommand<Help> GoToDetail { get=> new DelegateCommand<Help>(async (param) =>
        {
            await NavigateTo(param);
        }); }
        private string typeHelp;
        public HelpPageViewModel(INavigationService navigationService, IPageDialogService dialogService, ICovidUserServices userServices,IHelpServices helpServices) : base(navigationService, dialogService, userServices,helpServices)
        {

            GoToDetailCommand.Execute();
            LoadPins = new DelegateCommand(async () => await GetPerson());

        }
        public async Task GetPerson()
        {
            HelpsPerson = new ObservableCollection<Help>();
            if (typeHelp == $"{ETypeHelp.Medicamentos}")
            {
                var emergencia = await helpServices.GetHelp($"{ETypeHelp.Emergencia}",$"{EState.Activo}",Setting.Token);
                var asistencia = await helpServices.GetHelp($"{ETypeHelp.Asistencia_Medica.ToString().Replace('_',' ')}",$"{EState.Activo}", Setting.Token);
                var psicologica = await helpServices.GetHelp($"{ETypeHelp.Psicologica}",$"{EState.Activo}", Setting.Token);
                emergencia.AddRange(asistencia);
                emergencia.AddRange(psicologica);
                HelpsPerson = new ObservableCollection<Help>(emergencia);
            }
            else
            {
                var alimentos = await helpServices.GetHelp($"{ETypeHelp.Alimentos}", $"{EState.Activo}", Setting.Token);
                var medicamentos = await helpServices.GetHelp($"{ETypeHelp.Medicamentos}", $"{EState.Activo}", Setting.Token);
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
