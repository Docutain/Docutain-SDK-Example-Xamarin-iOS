using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public class NavigationController : UINavigationController
    {
        public NavigationController(UIViewController rootViewController) : base(rootViewController)
        {
            SetBarColors();
        }

        public override void TraitCollectionDidChange(UITraitCollection previousTraitCollection)
        {
            base.TraitCollectionDidChange(previousTraitCollection);
            if(UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                if (TraitCollection.HasDifferentColorAppearanceComparedTo(previousTraitCollection))
                    SetBarColors();
            }
        }

        private void SetBarColors()
        {
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                bool isDark = TraitCollection.UserInterfaceStyle == UIUserInterfaceStyle.Dark;
                var navbarAppearance = new UINavigationBarAppearance();
                var TopBarsBackgroundColor = isDark ? UIColor.FromRGB(42, 42, 42) : UIColor.FromRGB(76, 175, 80);
                var TopBarsForegroundColor = isDark ? UIColor.FromRGBA(255, 255, 255, 222) : UIColor.FromRGB(255, 255, 255);
                navbarAppearance.BackgroundColor = TopBarsBackgroundColor;
                navbarAppearance.TitleTextAttributes = new UIStringAttributes { ForegroundColor = TopBarsForegroundColor };
                NavigationBar.StandardAppearance = navbarAppearance;
                NavigationBar.ScrollEdgeAppearance = navbarAppearance;
            }
            else
            {
                // Fallback on earlier versions
                NavigationBar.BarTintColor = UIColor.FromRGB(76, 175, 80);
                NavigationBar.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.White };
            }
            NavigationBar.TintColor = UIColor.White;
        }
    }
}