﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;


namespace COVIDHelp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        private const string ExpandAnimationName = "ExpandAnimation";
        private const string CollapseAnimationName = "CollapseAnimation";
        private const double SlideAnimationDuration = 0.25;
        private const int AnimationDuration = 600;
        private const double PageScale = 0.9;
        private const double PageTranslation = 0.35;
        private readonly IEnumerable<View> _menuItemsView;
        private bool _isAnimationRun;
        private double _safeInsetsTop;


        public ProfilePage()
        {
            InitializeComponent();
            _menuItemsView = new[] { User, (View)editar, CerrarSeccion };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Device.RuntimePlatform == Device.iOS)
            {
                var safeInsets = On<iOS>().SafeAreaInsets();
                _safeInsetsTop = safeInsets.Top;
                ToolbarSafeAreaRow.Height = MenuSafeAreaRow.Height = _safeInsetsTop;
            }

            MenuTopRow.Height = MenuBottomRow.Height = Device.Info.ScaledScreenSize.Height * (1 - PageScale) / 2;
        }
        private async void OnShowMenu(object sender, EventArgs e)
        {
            if (_isAnimationRun)
                return;

            _isAnimationRun = true;
            var animationDuration = AnimationDuration;
            if (ProfileePage.Scale < 1)
            {
                animationDuration = (int)(AnimationDuration * SlideAnimationDuration);
                GetCollapseAnimation().Commit(this, CollapseAnimationName, 16,
                    (uint)(AnimationDuration * SlideAnimationDuration),
                    Easing.Linear,
                    null, () => false);
            }
            else
            {
                GetExpandAnimation().Commit(this, ExpandAnimationName, 16,
                    AnimationDuration,
                    Easing.Linear,
                    null, () => false);
            }

            await Task.Delay(animationDuration);
            _isAnimationRun = false;
        }


        private Animation GetExpandAnimation()
        {
            var iconAnimationTime = (1 - SlideAnimationDuration) / _menuItemsView.Count();
            var animation = new Animation
            {
                {0, SlideAnimationDuration, new Animation(v => ToolbarSafeAreaRow.Height = v, _safeInsetsTop, 0)},
                {
                    0, SlideAnimationDuration,
                    new Animation(v => ProfileePage.TranslationX = v, 0, Device.Info.ScaledScreenSize.Width * PageTranslation)
                },
                {0, SlideAnimationDuration, new Animation(v => ProfileePage.Scale = v, 1, PageScale)},
                {
                    0, SlideAnimationDuration,
                    new Animation(v => ProfileePage.Margin = new Thickness(0, v, 0, 0), 0, _safeInsetsTop)
                },
            };

            foreach (var view in _menuItemsView)
            {
                var index = _menuItemsView.IndexOf(view);
                animation.Add(SlideAnimationDuration + iconAnimationTime * index - 0.05,
                    SlideAnimationDuration + iconAnimationTime * (index + 1) - 0.05, new Animation(
                        v => view.Opacity = (float)v, 0, 1));
                animation.Add(SlideAnimationDuration + iconAnimationTime * index,
                    SlideAnimationDuration + iconAnimationTime * (index + 1), new Animation(
                        v => view.TranslationY = (float)v, -10, 0));
            }

            return animation;
        }
        private Animation GetCollapseAnimation()
        {
            var animation = new Animation
            {
                {0, 1, new Animation(v => ToolbarSafeAreaRow.Height = v, 0, _safeInsetsTop)},
                {0, 1, new Animation(v => ProfileePage.TranslationX = v, Device.Info.ScaledScreenSize.Width * PageTranslation, 0)},
                {0, 1, new Animation(v => ProfileePage.Scale = v, PageScale, 1)},
                {0, 1, new Animation(v => ProfileePage.Margin = new Thickness(0, v, 0, 0), _safeInsetsTop, 0)},
            };

            foreach (var view in _menuItemsView)
            {
                animation.Add(0, 1, new Animation(
                    v => view.Opacity = (float)v, 1, 0));
                animation.Add(0, 1, new Animation(
                    v => view.TranslationY = (float)v, 0, -10));
            }

            return animation;
        }
    }
}