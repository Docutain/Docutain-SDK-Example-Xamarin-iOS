using System;
using Foundation;
using UIKit;
//using DocutainSdk;
using CoreFoundation;
using static AVFoundation.AVMetadataIdentifiers;
using Docutain.SDK.Xamarin.iOS;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public class ViewControllerTextResult : UIViewController
    {
        private UIActivityIndicatorView loadingIndicator;
        private UITextView textView;
        private System.nfloat padding = 16.0f;
        private string filePath;

        public ViewControllerTextResult(string filePath)
        {
            this.filePath = filePath;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.BackButtonTitle = "";
            Title = "Docutain SDK Example";

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                View.BackgroundColor = UIColor.SystemBackground;
            else
                View.BackgroundColor = UIColor.White;

            loadingIndicator = new UIActivityIndicatorView();
            textView = new UITextView();
            textView.Editable = false;
            textView.Font = UIFont.SystemFontOfSize(UIFont.SystemFontSize);
            View.AddSubview(textView);
            textView.TranslatesAutoresizingMaskIntoConstraints = false;
            textView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor, padding).Active = true;
            textView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, -padding).Active = true;
            textView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor, padding).Active = true;
            textView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor, -padding).Active = true;

            loadingIndicator.HidesWhenStopped = true;
            loadingIndicator.StartAnimating();
            View.AddSubview(loadingIndicator);
            loadingIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
            loadingIndicator.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
            loadingIndicator.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor).Active = true;

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                LoadText();
            else
                Console.WriteLine("Text Recognition is only available for iOS 13+");
        }

        private void LoadText()
        {
            DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Default).DispatchAsync(() =>
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    //if a filePath is available it means we have imported a file. If so, we need to load it into the SDK first
                    if (!DocumentDataReader.LoadFile(NSUrl.FromFilename(filePath)))
                    {
                        //an error occurred, get the latest error message
                        Console.WriteLine($"DocumentDataReader.LoadFile failed with error: {DocutainSDK.LastError}");
                        return;
                    }
                }
                //get the text of all currently loaded pages
                //if you want text of just one specific page, define the page number
                //see https://docs.docutain.com/docs/Xamarin/textDetection for more details
                string text = DocumentDataReader.GetText();
                DispatchQueue.MainQueue.DispatchAsync(() =>
                {
                    loadingIndicator.StopAnimating();
                    textView.Text = text;
                });
            });
        }
    }
}
