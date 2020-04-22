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

namespace COVIDHelp.ViewModels
{
    public abstract class SelectAssistanceBaseViewModel : BaseViewModel
    {
        public ObservableCollection<Diseases> Diseases { get; set; }
        public Diseases DiseasesAdd { get; set; } = new Diseases();
        public DelegateCommand<string> AddDataAndNavigateCommand { get; set; }

        private bool isEnable;
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
        public SelectAssistanceBaseViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {

        }
        public async Task DisplayAction(string filtra)
        {
            bool action = await dialogService.DisplayAlertAsync("", "Estás seguro/a?", "Aceptar", "Cancelar");

            if (action)
            {
                Diseases.Add(DiseasesAdd);
                SaveData();
                await NavigateTo(filtra);
            }

            await navigationService.GoBackToRootAsync();
        }
        public void SaveData()
        {
            Barrel.ApplicationId = ConfigApi.MonkeyChadeKey;
            Barrel.Current.Add(key: $"DataUserAssistenceNurse", data: Diseases, expireIn: TimeSpan.FromDays(31));
        }
        public async Task NavigateTo(string filtra)
        {
            Int64 cedula = 0;
            var user = await apiCovitServices.FindUser(cedula.GetPreferencesInt("Cedula"));
            if (user != null)
            {
                var help = new Help
                {
                    Nombre = $"{user.Nombres} {user.Apellidos}",
                    Cedula = user.Cedula,
                    Email = user.Correo,
                    Telefono = user.Telefono,
                    Posicion = $"{user.Latitude},{user.Longitude}",
                    Dirrecion = user.Direccion,
                    Status = $"{EState.Activo}",
                    FechaEnviado = DateTime.Now,
                    Tipo = filtra,

                };
                var diseases = Diseases.Select(e => e.Name);
                help.DescripcionProblema = string.Join("\n",diseases.ToArray());
                var probar = await apiCovitServices.PostHelp(help);
                await navigationService.GoBackToRootAsync();
            }

        }
    }

}

