using COVIDHelp.Services;
using COVIDHelp.ViewModels;
using COVIDHelp.ViewModels.LoginAndRegisterViewModels;
using COVIDHelp.Views;
using COVIDHelp.Views.HelpersViews;
using COVIDHelp.Views.LoginAndRegisterView;
using COVIDHelp.Views.NeededViews;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;

namespace COVIDHelp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();
<<<<<<< HEAD
            NavigationService.NavigateAsync(new Uri($"/NavigationPage{NavigationConstants.LoginPage}"));

=======
            NavigationService.NavigateAsync(new Uri($"{NavigationConstants.LoginPage}", UriKind.Absolute));
>>>>>>> e5a2ddcafcf67f52a494b511595a5894d82b5bc4
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MapsPage, MapsPageViewModel>();
            containerRegistry.RegisterForNavigation<HelpersMainPage>();
            containerRegistry.RegisterForNavigation<LocationPermitionPage>();
            containerRegistry.RegisterForNavigation<HelpPage>();
            containerRegistry.RegisterInstance<IApiCovitServices>(new ApiCovitServices());
            containerRegistry.RegisterInstance<IApiGoogleServices>(new ApiGoogleServices());
            containerRegistry.RegisterForNavigation<HomePage>();
            containerRegistry.RegisterForNavigation<CommitmentsPage, CommitmentsPageViewModel>();
            containerRegistry.RegisterForNavigation<LocationPermitionPage, LocationPermitionPageViewModel>();

        }
    }

}
