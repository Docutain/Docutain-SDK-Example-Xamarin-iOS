using System;
using UIKit;
using Foundation;
using CoreAudioKit;
using CoreFoundation;
using Docutain;
using Docutain.SDK.Xamarin.iOS;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public enum LogLevel
    {
        NONE,
        ERROR,
        VERBOSE,
        INFO
    }


    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;
        string licenseKey = "YOUR_LICENSE_KEY_HERE";


        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            InitRootWindow();

            // The Docutain SDK needs to be initialized prior to using any functionality
            // A valid license key is required (contact us via [mailto:sdk@Docutain.com] to get a trial license)
            if (!DocutainSDK.InitSDK(licenseKey))
            {
                // Initialization of Docutain SDK failed, get last error message
                Console.WriteLine($"InitSDK failed with error: {DocutainSDK.LastError}");
                if (licenseKey == "YOUR_LICENSE_KEY_HERE")
                {
                    ShowLicenseEmptyInfo();
                    return false;
                }
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                // If you want to use text recognition (OCR) and/or data extraction features, you need to set the AnalyzeConfiguration
                // in order to start all the necessary processes
                var analyzeConfig = new AnalyzeConfiguration
                {
                    ReadPaymentState = true,
                    ReadBIC = true
                };

                if (!DocumentDataReader.SetAnalyzeConfiguration(analyzeConfig))
                {
                    Console.WriteLine($"setAnalyzeConfiguration failed with error: {DocutainSDK.LastError}");
                }
            }

            // Depending on your needs, you can set the Logger's levels
            Logger.SetLogLevel(Level.Verbose);

            // Depending on the log level that you have set, some temporary files get written on the filesystem
            // You can delete all temporary files by using the following method
            DocutainSDK.DeleteTempFiles(true);

            return true;
        }

        private void InitRootWindow()
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);
            var viewController = new ViewController();
            var navigationController = new NavigationController(viewController);
            window.RootViewController = navigationController;
            window.MakeKeyAndVisible();
        }        

        private void ShowLicenseEmptyInfo()
        {
            var alert = UIAlertController.Create("License empty", "A valid license key is required. Please contact us via sdk@Docutain.com to get a trial license.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Get license", UIAlertActionStyle.Default, action =>
            {
                var email = "sdk@Docutain.com";
                var subject = "Trial%20License%20Request";
                var mailURLString = $"mailto:{email}?subject={subject}"; //.UrlEncode(NSUrlUtilities.AllowedCharacters); TODO
                var mailURL = NSUrl.FromString(mailURLString);
                if (UIApplication.SharedApplication.CanOpenUrl(mailURL))
                    UIApplication.SharedApplication.OpenUrl(mailURL);
                else
                    Console.WriteLine("Mail cannot be opened");
                window.UserInteractionEnabled = false;
            }));
            alert.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, action => { Environment.Exit(0); }));
            window.RootViewController.PresentViewController(alert, true, null);
        }
    }
}