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
    public class SelectAssistenceDoctorViewModel:BaseViewModel
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
        public SelectAssistenceDoctorViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices, IApiGoogleServices apiGoogleServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Diseases = new ObservableCollection<Diseases>()
            {
                new Diseases { Name = "Fiebre" },
                new Diseases { Name = "Dolor de cabeza" },
                new Diseases { Name = "Dificultad para respirar" },
            };

            AddDataAndNavigateCommand = new DelegateCommand(async () =>
            {
                AddDiseases();
               await  NavigateTo();

            });
        }

        void AddDiseases()
        {
            Diseases.Add(DiseasesAdd);
        }
        async Task NavigateTo()
        {
            Int64 cedula = 0;
            var user = await apiCovitServices.FindUser(cedula.GetPreferencesInt("Cedula"));
            if (user!=null)
            {
                var help = new Help
                {
                    Nombre = user.Nombres,
                    Cedula = user.Cedula,
                    Email = user.Correo,
                    Telefono = user.Telefono,
                    Posicion = $"{user.Latitude},{user.Longitude}",
                    Dirrecion = user.Direccion,
                    Status="Activo"
                   
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
