﻿using COVIDHelp.Helpers;
using COVIDHelp.Models;
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
            if (Setting.IsLoggedExists())
            {
                var param = new Prism.Navigation.NavigationParameters
                {
                    { Constants.PersonKey, Setting.Logged() }
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
            containerRegistry.RegisterForNavigation<SignUpPage, SignUpPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProfilePage, ProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<MapsPage, MapsPageViewModel>();
            containerRegistry.RegisterForNavigation<HelpersMainPage>();
            containerRegistry.RegisterForNavigation<HelpPage, HelpPageViewModel>();
            containerRegistry.RegisterInstance<IApiCovitServices>(new ApiCovitServices());
            containerRegistry.RegisterInstance<IApiGoogleServices>(new ApiGoogleServices());
            containerRegistry.RegisterForNavigation<HomePage, ViewModels.HomePageViewModel>();
            containerRegistry.RegisterForNavigation<CommitmentsPage, CommitmentsPageViewModel>();
            containerRegistry.RegisterForNavigation<LocationPermitionPage, LocationPermitionPageViewModel>();
            containerRegistry.RegisterForNavigation<RequestDetailPage, RequestDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<MedicalAssistenceRequestPage, MedicalAssistenceRequestPageViewModel>();
            containerRegistry.RegisterForNavigation<IdentificationPage, IdentificationPageViewModel>();
            containerRegistry.RegisterForNavigation<NecesityDetailPage, NecesityDetailPageViewModel>();
            containerRegistry.RegisterForNavigation<SelectAssistenceDoctor, SelectAssistenceDoctorViewModel>();
            containerRegistry.RegisterForNavigation<SelectAssistenceNurse, SelectAssistenceNurseViewModel>();
            containerRegistry.RegisterForNavigation<SelectAssistencePsycology, SelectAssistencePsycologyViewModel>();
            containerRegistry.RegisterForNavigation<EditProfilePage, EditProfilePageViewModel>();
            containerRegistry.RegisterForNavigation<RequestsListPage, RequestsListPageViewModel>();
            containerRegistry.RegisterForNavigation<DoItForMePage, DoItForMePageViewModel>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterDialog<EmergencyPage, EmergencyPageViewModel>();

        }
        
    }
}
