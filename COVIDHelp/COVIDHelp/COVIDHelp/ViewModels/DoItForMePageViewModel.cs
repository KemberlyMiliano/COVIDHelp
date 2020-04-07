using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace COVIDHelp.ViewModels
{
    public class DoItForMePageViewModel : BaseViewModel
    {
        public Request Need { get; set; } = new Request();
        public ObservableCollection<Request> Requests { get; set; } = new ObservableCollection<Request>();
        public DelegateCommand AddToGlobalRequests { get; set; }
        public DelegateCommand AddToList { get; set; }
        public DoItForMePageViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices) : base(navigationService, dialogService, apiCovitServices)
        {
            AddToGlobalRequests = new DelegateCommand(async () =>
            {
                //anadir a la lista global de requerimientos para voluntarios que no sean profesionales de la salud
            });

            AddToList = new DelegateCommand(() =>
            {
                Requests.Add(Need);
            });

        }
    }
}
