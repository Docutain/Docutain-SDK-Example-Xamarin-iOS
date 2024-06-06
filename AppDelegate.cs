using System;
using UIKit;
using Foundation;
using CoreAudioKit;
using CoreFoundation;
using Docutain;
using Docutain.SDK.Xamarin.iOS;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;

        //A valid license key is required, you can generate one on our website https://sdk.docutain.com/TrialLicense?Source=1376772
        string licenseKey = "YOUR_LICENSE_KEY_HERE";


        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            InitRootWindow();

            // The Docutain SDK needs to be initialized prior to using any functionality
            //a valid license key is required, you can generate one on our website https://sdk.docutain.com/TrialLicense?Source=1376772
            if (!DocutainSDK.InitSDK(licenseKey))
            {
                // Initialization of Docutain SDK failed, get last error message
                Console.WriteLine($"InitSDK failed with error: {DocutainSDK.LastError}");
                if (licenseKey == "YOUR_LICENSE_KEY_HERE")
                {
                    ShowLicenseEmptyInfo();
                    return false;
                }
                else
                {
                    ShowLicenseErrorInfo();
                    return false;
                }
            }

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                //Reading payment state and BIC when getting the analyzed data is disabled by default
                //If you want to analyze these 2 fields as well, you need to set the AnalyzeConfig accordingly
                //A good place to do this, is right after initializing the Docutain SDK
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
            var alert = UIAlertController.Create("License empty", "A valid license key is required. Please click \"Get License\" in order to create a free" +
                " trial license key on our website.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Get License", UIAlertActionStyle.Default, action =>
            {
                var mailURL = NSUrl.FromString("https://sdk.docutain.com/TrialLicense?Source=1376772");
                UIApplication.SharedApplication.OpenUrl(mailURL);
                ShowLicenseEmptyInfo(); //keep info popup open
            }));
            window.RootViewController.PresentViewController(alert, true, null);
        }

        private void ShowLicenseErrorInfo()
        {
            var alert = UIAlertController.Create("License error",
                "A valid license key is required. Please contact our support to get an extended trial license.",
                UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Contact Support", UIAlertActionStyle.Default, action =>
            {
                SendEmailToSupport(() =>
                {
                    ShowLicenseErrorInfo(); //keep info popup open
                });
            }));
            window.RootViewController.PresentViewController(alert, true, null);
        }

        private void SendEmailToSupport(Action completion)
        {
            var mailtoString = $"mailto:support.sdk@Docutain.com?subject=Trial License Error&body=Please keep your following trial license key in this e-mail: {licenseKey}"
                .Replace(" ", "%20");

            var mailtoUrl = new NSUrl(mailtoString);
            if (UIApplication.SharedApplication.CanOpenUrl(mailtoUrl))
            {
                UIApplication.SharedApplication.OpenUrl(mailtoUrl, new NSDictionary(), success =>
                {
                    completion();
                });
            }
        }
    }
}