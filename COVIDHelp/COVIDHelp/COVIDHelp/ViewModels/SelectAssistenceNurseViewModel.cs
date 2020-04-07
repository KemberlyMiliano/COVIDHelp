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
    public class SelectAssistenceNurseViewModel : BaseViewModel
    {
        public ObservableCollection<Diseases> Diseases { get; set; }
        public Diseases DiseasesAdd { get; set; } = new Diseases();
        public DelegateCommand AddDataAndNavigateCommand { get; set; }
        private bool isEnable;

        public bool IsEnable
        {
            get
            {

                return isEnable;


            }
            set
            {
                isEnable = value;

            }
        }
        public SelectAssistenceNurseViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Diseases = new ObservableCollection<Diseases>()
            {
                new Diseases { Name = "Hipertension" },
                new Diseases { Name = "Diabetes" },
                new Diseases { Name = "Presion Arterial" },
            };

            AddDataAndNavigateCommand = new DelegateCommand(async () =>
            {
                AddDiseases();
                SaveData();
                await NavigateTo();

            });
        }

        void AddDiseases()
        {
            Diseases.Add(DiseasesAdd);
        }
        void SaveData()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyChadeKey;
            Barrel.Current.Add(key: $"DataUserAssistenceNurse", data: Diseases, expireIn: TimeSpan.FromDays(31));
        }
        async Task NavigateTo()
        {
            Int64 cedula = 0;
            var user = await apiCovitServices.FindUser(cedula.GetPreferencesInt("Cedula"));
            if (user != null)
            {
                var help = new Help
                {
                    Nombre = user.Nombres,
                    Cedula = user.Cedula,
                    Email = user.Correo,
                    Telefono = user.Telefono,
                    Posicion = $"{user.Latitude},{user.Longitude}",
                    Dirrecion = user.Direccion,
                    Status = "Activo"

                };
                foreach (var item in Diseases)
                {
                    if (item.IsEnable)
                    {
                        help.DescripcionProblema += $"{item.Name}\n";
                    }

                }
                var probar = await apiCovitServices.PostHelp(help);
                await navigationService.GoBackToRootAsync();
            }

        }
    }
}
