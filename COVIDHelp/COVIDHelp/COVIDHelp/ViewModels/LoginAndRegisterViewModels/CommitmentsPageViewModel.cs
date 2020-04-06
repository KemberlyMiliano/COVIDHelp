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
using Xamarin.Forms.OpenWhatsApp;

namespace COVIDHelp.ViewModels.LoginAndRegisterViewModels
{
    public class CommitmentsPageViewModel : BaseViewModel
    {
        public DelegateCommand ContactCommand { get; set; }
        public DelegateCommand DoneCommand { get; set; }
        public ObservableCollection<Necesity> Historial { get; set; } = new ObservableCollection<Necesity>();
        public CommitmentsPageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            Historial.Add(new Necesity { Image = "home", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "profile", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "home", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "profile", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "home", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "profile", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "home", NeededPerson = "Rosa", Status = "Mal" });
            Historial.Add(new Necesity { Image = "profile", NeededPerson = "Rosa", Status = "Mal" });

            ContactCommand = new DelegateCommand(async () =>
            {
                await OpenWhatsApp("+1 829 465 8565", "Hola! Estoy aquí para ayudarte");

            });

            DoneCommand = new DelegateCommand(() =>
            {


            });
        }
        private async Task OpenWhatsApp(string number, string text)
        {
            try
            {
                Chat.Open(number, text);
            }
            catch (Exception ex)
            {
                await dialogService.DisplayAlertAsync("Error", ex.Message, "OK");
            }
        }
    }

}
