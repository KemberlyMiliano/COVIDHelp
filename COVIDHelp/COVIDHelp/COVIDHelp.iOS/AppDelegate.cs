﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Plugin.GoogleClient;
using Prism;
using Prism.Ioc;
using UIKit;

namespace COVIDHelp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
            global::Xamarin.Forms.Forms.SetFlags("CarouselView_Experimental");
            GoogleClientManager.Initialize();
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsGoogleMaps.Init("AIzaSyBvYUEfTGHvXCXaPeMRqwIZHxt3e8EjpAk");
            Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();
            LoadApplication(new App(new iOSInitializer()));
            return base.FinishedLaunching(app, options);
        }
        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            return GoogleClientManager.OnOpenUrl(app, url, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
