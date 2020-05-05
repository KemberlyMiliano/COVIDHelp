using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace COVIDHelp.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged,IDestructible
    {
        protected INavigationService navigationService;
        protected IPageDialogService dialogService;
        protected IApiCovitServices apiCovitServices;
        public bool IsMapEnable { get; set; } 
        ApiOfflineCovidServices OfflineCovidServices = new ApiOfflineCovidServices();
        public bool IsNotConnected { get; set; }
        public BaseViewModel(INavigationService navigationService, IPageDialogService dialogService, IApiCovitServices apiCovitServices)
        {
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.apiCovitServices = apiCovitServices;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            IsNotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            IsMapEnable = !IsNotConnected && Setting.MapsSetting ? true : false;
        }
        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsNotConnected = e.NetworkAccess != NetworkAccess.Internet;
            Setting.IsNotConnected = IsNotConnected;
            IsMapEnable = !IsNotConnected&&Setting.MapsSetting ? true : false;
            if (!IsNotConnected)
            {
                   await OfflineCovidServices.PostHelpOffline();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected async Task<Help> PostOffline(Help help)
        {
            await OfflineCovidServices.PostHelpOffline(help);
            return help;
        }
        public void Destroy()
        {
            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }
    }
}
