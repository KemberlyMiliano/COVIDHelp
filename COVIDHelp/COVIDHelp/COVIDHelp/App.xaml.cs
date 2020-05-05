using COVIDHelp.Helpers;
using COVIDHelp.Models;
using COVIDHelp.Services;
using COVIDHelp.ViewModels;
using COVIDHelp.ViewModels.DialogServices;
using COVIDHelp.ViewModels.LoginAndRegisterViewModels;
using COVIDHelp.Views;
using COVIDHelp.Views.DailogsViews;
using COVIDHelp.Views.HelpersViews;
using COVIDHelp.Views.LoginAndRegisterView;
using COVIDHelp.Views.NeededViews;
using MonkeyCache.FileStore;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
namespace COVIDHelp
{
    public partial class App : PrismApplication
    {
      
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        protected override void OnInitialized()
        {
            InitializeComponent();
            if (Setting.Token!=null)
            {
                var param = new Prism.Navigation.NavigationParameters
                {
                    { Constants.IdKey, Setting.Id },
                    { Constants.Token, Setting.Token }
                };
                NavigationService.NavigateAsync(new Uri($"{NavigationConstants.HelpersMainPage}", UriKind.Absolute),param);
            }
            else
            {
                NavigationService.NavigateAsync(new Uri($"{NavigationConstants.LoginPage}", UriKind.Absolute));
            }
        
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MapsPage, MapsPageViewModel>();
            containerRegistry.RegisterForNavigation<HelpersMainPage>();
            containerRegistry.RegisterForNavigation<HelpPage, HelpPageViewModel>();
            containerRegistry.RegisterInstance<IApiCovitServices>(new ApiCovitServices());
            containerRegistry.RegisterInstance<IApiGoogleServices>(new ApiGoogleServices());
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<CommitmentsPage, CommitmentsPageViewModel>();   
            containerRegistry.RegisterForNavigation<LocationPermitionPage, LocationPermitionPageViewModel>();
            containerRegistry.RegisterForNavigation<RequestDetailPage, RequestDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MedicalAssistenceRequestPage, MedicalAssistenceRequestPageViewModel>();
            containerRegistry.RegisterForNavigation<IdentificationPage, IdentificationPageViewModel>();
            containerRegistry.RegisterForNavigation<NecesityDetailPage, NecesityDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<EditProfilePage, EditProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<RequestsListPage, RequestsListPageViewModel>();
            containerRegistry.RegisterForNavigation<DoItForMePage, DoItForMePageViewModel>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterDialog<EmergencyPage, EmergencyPageViewModel>();
            containerRegistry.RegisterDialog<DialogRateView, DialogRateViewModel>();
            containerRegistry.RegisterForNavigation<SelectAssistence, SelectAssistanceViewModel>(); 

        }
        
    }
}
