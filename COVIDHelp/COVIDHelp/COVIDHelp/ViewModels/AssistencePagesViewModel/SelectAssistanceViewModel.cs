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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace COVIDHelp.ViewModels
{
    public  class SelectAssistanceViewModel : BaseViewModel, IInitialize
    {
        public ObservableCollection<Diseases> Diseases { get; set; }
        public Diseases DiseasesAdd { get; set; } = new Diseases();
        public DelegateCommand AddDataAndNavigateCommand { get; set; }

        private bool isEnable;
        public string TypeHelp { get; set; }
        public bool IsEnable
        {
            get
            {
                return this.isEnable;
            }
            set
            {
                this.isEnable = value;
            }
        }
        public SelectAssistanceViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            AddDataAndNavigateCommand = new DelegateCommand(async () =>
            {
                await DisplayAction();
                await NavigateTo(TypeHelp);
            });
        }
        void TypeHelper()
        {
            Diseases = new ObservableCollection<Diseases>();
            string [] enums;
            var diseases = Enum.Parse(typeof(ETypeHelp), TypeHelp);
            switch (diseases)
            {
                case ETypeHelp.Emergencia:
                     enums = Enum.GetNames(typeof(EDisease));
                    enums.ForEach(e => Diseases.Add(new Diseases { Name = e.Replace('_', ' ') }));
                    break;
                case ETypeHelp.Psicologica:
                    enums = Enum.GetNames(typeof(EDiseasePsycology));
                    enums.ForEach(e => Diseases.Add(new Diseases { Name = e.Replace('_', ' ') }));
                    break;
                case ETypeHelp.Asistencia_Medica:
                    enums = Enum.GetNames(typeof(EDiseaseNurse));
                    enums.ForEach(e => Diseases.Add(new Diseases { Name = e.Replace('_', ' ') }));
                    break;
                default:
                    break;
            }
        } 
         async Task DisplayAction()
        {
            bool action = await dialogService.DisplayAlertAsync("", "Estás seguro/a?", "Aceptar", "Cancelar");

            if (!string.IsNullOrEmpty(DiseasesAdd.Name) &&action)
            {
                var diases = DiseasesAdd.Name.Split(',').ToArray();
                diases.ForEach(e=>Diseases.Add(new Diseases { Name=e,IsEnable = true}));
                
            }

            await navigationService.GoBackToRootAsync();
        }
        public async Task NavigateTo(string filtra)
        {   
            User user = await apiCovitServices.FindUser(Constants.IdKey, Setting.Id, Setting.Token);
            if (user != null)
            {
                Help help = new Help
                {
                     UserID = user.Id,      
                     Date = DateTime.Now,
                     Type = filtra,
                    Status = $"{EState.Activo}",
                    Latitude = user.Latitude,
                    Longitude = user.Longitude,
                    Address = user.Address

                };  
                var diseases = Diseases.Select(e => e.Name);
                help.Description = string.Join("\n",diseases.ToArray());
                var probar = !IsNotConnected? await apiCovitServices.PostHelp(help,Setting.Token) : await PostOffline(help);
                await navigationService.NavigateAsync(new Uri($"{NavigationConstants.HelpersMainPage}?selectedTab={NavigationConstants.RequestsListPage}", UriKind.Absolute));
            }

        }

        public void Initialize(INavigationParameters parameters)
        {
            var param = parameters[Constants.TypeHelp] as string;
            TypeHelp = param;
            TypeHelper();
         }
    }

}

