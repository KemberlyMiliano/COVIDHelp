using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace COVIDHelp.ViewModels
{
    public class DoItForMePageViewModel : BaseViewModel
    {
        public ObservableCollection<Request> Requests { get; set; } = new ObservableCollection<Request>();
        public DelegateCommand AddToGlobalRequests { get; set; }
        public string Text { get; set; }
        public DelegateCommand<Request> OnDeleteCommand { get; set; }
        public DelegateCommand AddToList { get; set; }
        public DoItForMePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            AddToGlobalRequests = new DelegateCommand(async () =>
            {
                await DisplayAction();
            });

            AddToList = new DelegateCommand(() =>
            {
                if (!string.IsNullOrEmpty(Text))
                {
                    Text.Split(',').ForEach(e => Requests.Add(new Request
                    {
                        Text = e
                    }));
                }

            });

            OnDeleteCommand = new DelegateCommand<Request>((param) =>
            {
                Requests.Remove(param);
            });

        }
        public async Task DisplayAction()
        {
            bool action = await dialogService.DisplayAlertAsync("", "Estás seguro/a?", "Aceptar", "Cancelar");

            if (action)
            {
                var user = await apiCovitServices.FindUser( Constants.IdKey, Setting.Id, Setting.Token);
                string status = "";
                    var help = new Help 
                    {
                        Id = user.Id,
                        Status = $"{EState.Activo}",
                        Date = DateTime.Now,
                        Type = status.GetPreferences("status"),
                        Longitude=user.Longitude,
                        Latitude = user.Latitude,
                        Address = user.Address
                    };
                   var request = Requests.Select(e => e.Text);
                    help.Description = string.Join("\n", request.ToArray());
                   await apiCovitServices.PostHelp(help,Setting.Token);
                    await navigationService.GoBackToRootAsync();
            }
        }
    }
}
