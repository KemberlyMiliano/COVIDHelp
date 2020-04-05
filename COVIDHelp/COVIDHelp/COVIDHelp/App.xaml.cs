using COVIDHelp.Services;
using COVIDHelp.ViewModels;
using COVIDHelp.ViewModels.HelpersViewModels;
using COVIDHelp.ViewModels.NeededViewModels;
using COVIDHelp.Views;
using COVIDHelp.Views.HelpersViews;
using COVIDHelp.Views.NeededViews;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;

namespace COVIDHelp
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync(new Uri("/NeededTabbedPage", UriKind.Relative));

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<SelectedSignUpPage, SelectedSignUpPageViewModel>();

            containerRegistry.RegisterForNavigation<NeededHomePage, NeededHomePageViewModel>();
 
            containerRegistry.RegisterForNavigation<CommitmentsPage, CommitmentsPageViewModel>();
            containerRegistry.RegisterForNavigation<MapsPage, MapsPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectNeededPage>();
            containerRegistry.RegisterForNavigation<HelpersMainPage>();
            containerRegistry.RegisterForNavigation<HelpPage, HelpersHomePageViewModel>();

        }
    }

}
