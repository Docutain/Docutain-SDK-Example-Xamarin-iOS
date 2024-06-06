using Foundation;
using UIKit;
using Docutain.SDK.Xamarin.iOS;
using System;
using System.Collections.Generic;
using CoreFoundation;
using static Docutain_SDK_Example_Xamarin_iOS.ViewController;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.Linq;
using CoreGraphics;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public partial class ViewController : UIViewController, IUITableViewDelegate, IUIDocumentInteractionControllerDelegate
    {
        public struct ListItem
        {
            public string Title { get; set; }
            public string Icon { get; set; }
            public string Subtitle { get; set; }
            public ItemType ItemType { get; set; }
        }

        public enum ItemType
        {
            None,
            DocumentScan,
            DataExtraction,
            TextRecognition,
            PDFGenerating,
            Settings
        }

        private string cellReuseIdentifier = "cell";
        private UITableView tableView;
        private ItemType selectedOption = ItemType.None;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.BackButtonTitle = "";
            Title = "Docutain SDK Example";

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                View.BackgroundColor = UIColor.SystemBackground;
            else
                View.BackgroundColor = UIColor.White;

            tableView = new UITableView();
            View.AddSubview(tableView);

            tableView.TranslatesAutoresizingMaskIntoConstraints = false;
            tableView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor).Active = true;
            tableView.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor).Active = true;
            tableView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor).Active = true;
            tableView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor).Active = true;

            tableView.RegisterClassForCellReuse(typeof(TableViewCell), cellReuseIdentifier);

            MyTableViewSource source = new MyTableViewSource();
            tableView.Source = source;
            source.ListItemClicked += Source_ListItemClicked;
        }

        private void Source_ListItemClicked(object sender, ListItem e)
        {
            switch (e.ItemType)
            {
                case ItemType.DocumentScan:
                    selectedOption = ItemType.None;
                    StartScan();
                    break;
                case ItemType.DataExtraction:
                    selectedOption = ItemType.DataExtraction;
                    StartDataExtraction();
                    break;
                case ItemType.TextRecognition:
                    selectedOption = ItemType.TextRecognition;
                    StartTextRecognition();
                    break;
                case ItemType.PDFGenerating:
                    selectedOption = ItemType.PDFGenerating;
                    StartPDFGenerating();
                    break;
                case ItemType.Settings:
                    selectedOption = ItemType.None;
                    ShowSettings();
                    break;
                default:
                    selectedOption = ItemType.None;
                    Console.WriteLine("Invalid item selected");
                    break;
            }
        }

        private void ShowSettings()
        {
            NavigationController.PushViewController(new ViewControllerSettings(), true);
        }

        private async void StartScan()
        {
            //define a DocumentScannerConfiguration to alter the scan process and define a custom theme to match your branding
            var scanConfig = GetScanConfig();
            // Start the scanner using the provided config
            bool success = await UI.ScanDocument(scanConfig);
            if (success)
                ProceedBasedOnCurrentSelection(null);
            else
                Console.WriteLine("canceled scan process");
        }

        private DocumentScannerConfiguration GetScanConfig(bool imageImport = false)
        {
            //There are a lot of settings to configure the scanner to match your specific needs
            //Check out the documentation to learn more https://docs.docutain.com/docs/Xamarin/docScan#change-default-scan-behaviour

            var scanConfig = new DocumentScannerConfiguration();

            if (imageImport)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(14, 0))
                {
                    scanConfig.Source = Source.GalleryMultiple;
                }
                else
                {
                    scanConfig.Source = Source.Gallery;
                }
            }
            //In this sample app we provide a settings page which the user can use to alter the scan settings
            //The settings are stored in and read from "DocutainPreferences"
            //This is supposed to be just an example, you do not need to implement it in that exact way
            //If you do not want to provide your users the possibility to alter the settings themselves at all
            //You can just set the settings according to the apps needs

            // Set scan settings
            scanConfig.AllowCaptureModeSetting = DocutainPreferences.AllowCaptureModeSetting;
            scanConfig.AutoCapture = DocutainPreferences.AutoCapture;
            scanConfig.AutoCrop = DocutainPreferences.AutoCrop;
            scanConfig.MultiPage = DocutainPreferences.MultiPage;
            scanConfig.DefaultScanFilter = DocutainPreferences.DefaultScanFilter;

            // Set edit settings
            scanConfig.PageEditConfig.AllowPageFilter = DocutainPreferences.AllowPageFilter;
            scanConfig.PageEditConfig.AllowPageRotation = DocutainPreferences.AllowPageRotation;
            scanConfig.PageEditConfig.AllowPageCropping = DocutainPreferences.AllowPageCropping;
            scanConfig.PageEditConfig.AllowPageArrangement = DocutainPreferences.AllowPageArrangement;
            scanConfig.PageEditConfig.PageArrangementShowPageNumber = DocutainPreferences.PageArrangementShowPageNumber;
            scanConfig.PageEditConfig.PageArrangementShowDeleteButton = DocutainPreferences.PageArrangementShowDeleteButton;

            // Set color settings
            scanConfig.ColorConfig.SetColorPrimary(DocutainPreferences.ColorPrimary["light"].ToString().ToUIColor(), DocutainPreferences.ColorPrimary["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorSecondary(DocutainPreferences.ColorSecondary["light"].ToString().ToUIColor(), DocutainPreferences.ColorSecondary["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorOnSecondary(DocutainPreferences.ColorOnSecondary["light"].ToString().ToUIColor(), DocutainPreferences.ColorOnSecondary["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorScanButtonsLayoutBackground(DocutainPreferences.ColorScanButtonsLayoutBackground["light"].ToString().ToUIColor(), DocutainPreferences.ColorScanButtonsLayoutBackground["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorScanButtonsForeground(DocutainPreferences.ColorScanButtonsForeground["light"].ToString().ToUIColor(), DocutainPreferences.ColorScanButtonsForeground["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorScanPolygon(DocutainPreferences.ColorScanPolygon["light"].ToString().ToUIColor(), DocutainPreferences.ColorScanPolygon["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorBottomBarBackground(DocutainPreferences.ColorBottomBarBackground["light"].ToString().ToUIColor(), DocutainPreferences.ColorBottomBarBackground["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorBottomBarForeground(DocutainPreferences.ColorBottomBarForeground["light"].ToString().ToUIColor(), DocutainPreferences.ColorBottomBarForeground["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorTopBarBackground(DocutainPreferences.ColorTopBarBackground["light"].ToString().ToUIColor(), DocutainPreferences.ColorTopBarBackground["dark"].ToString().ToUIColor());
            scanConfig.ColorConfig.SetColorTopBarForeground(DocutainPreferences.ColorTopBarForeground["light"].ToString().ToUIColor(), DocutainPreferences.ColorTopBarForeground["dark"].ToString().ToUIColor());

            // Alter the onboarding image source if you like
            // scanConfig.OnboardingImageSource = ...

            // Detailed information about theming possibilities can be found here: https://docs.docutain.com/docs/Xamarin/theming
            return scanConfig;
        }


        private async void StartPDFImport()
        {
            var pdfFile = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Pdf
            });
            if (pdfFile != null)
                ProceedBasedOnCurrentSelection(pdfFile.FullPath);
            else
                Console.WriteLine("canceled PDF import");
        }

        private async void StartImageImport()
        {
            // Define a DocumentScannerConfiguration to alter the scan process and define a custom theme to match your branding
            var scanConfig = GetScanConfig(imageImport: true);
            // Start the scanner using the provided config
            bool success = await UI.ScanDocument(scanConfig);
            if (success)
                ProceedBasedOnCurrentSelection(null);
            else
                Console.WriteLine("canceled scan process");
        }


        private void StartDataExtraction()
        {
            ShowInputOptionAlert();
        }

        private void StartTextRecognition()
        {
            ShowInputOptionAlert();
        }

        private void StartPDFGenerating()
        {
            ShowInputOptionAlert();
        }

        private void ShowInputOptionAlert()
        {
            var alert = UIAlertController.Create("Info", "input_option_message".Localized(), UIAlertControllerStyle.ActionSheet);

            if (alert.PopoverPresentationController != null)
            {
                alert.PopoverPresentationController.SourceView = this.View;
                alert.PopoverPresentationController.SourceRect = new CoreGraphics.CGRect(this.View.Bounds.GetMidX(), this.View.Bounds.GetMidY(), 0, 0);
                alert.PopoverPresentationController.PermittedArrowDirections = 0;
            }

            alert.AddAction(UIAlertAction.Create("input_option_scan".Localized(), UIAlertActionStyle.Default, action => StartScan()));
            alert.AddAction(UIAlertAction.Create("input_option_PDF".Localized(), UIAlertActionStyle.Default, action => StartPDFImport()));
            alert.AddAction(UIAlertAction.Create("input_option_Image".Localized(), UIAlertActionStyle.Default, action => StartImageImport()));
            alert.AddAction(UIAlertAction.Create("Cancel".Localized(), UIAlertActionStyle.Cancel, null));
            PresentViewController(alert, true, null);
        }

        private void GeneratePDF(string filePath)
        {
            Task.Run(() =>
            {
                if (!string.IsNullOrEmpty(filePath))
                {
                    //if a filePath is available it means we have imported a file. If so, we need to load it into the SDK first
                    if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                    {
                        //searchable PDF files are only available for iOS 13+
                        if (!DocumentDataReader.LoadFile(NSUrl.FromFilename(filePath)))
                        {
                            //an error occured, get the latest error message
                            Console.WriteLine($"DocumentDataReader.LoadFile failed with error: {DocutainSDK.LastError}");
                            return;
                        }
                    }
                    else
                    {
                        //load for non searchable PDF
                        if (!Document.LoadFile(NSUrl.FromFilename(filePath)))
                        {
                            //an error occured, get the latest error message
                            Console.WriteLine($"Document.LoadFile failed with error: {DocutainSDK.LastError}");
                            return;
                        }
                    }
                }

                //define the output path for the PDF
                var paths = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User);
                var destinationPath = paths[0];
                //generate the PDF from the currently loaded document
                //the generated PDF also contains the detected text, making the PDF searchable
                //see https://docs.docutain.com/docs/Xamarin/pdfCreation for more details
                var pdfUrl = Document.WritePDF(destinationPath, "DocutainSDK", true, PDFPageFormat.A4);
                if (pdfUrl != null)
                {
                    //displaying the generated PDF for demonstration purposes
                    DispatchQueue.MainQueue.DispatchAsync(() =>
                    {
                        var dc = UIDocumentInteractionController.FromUrl(pdfUrl);
                        dc.Delegate = this;
                        dc.PresentPreview(true);
                    });
                }
                else
                {
                    Console.WriteLine($"Writing PDF file failed, last error: {DocutainSDK.LastError}");
                }
            });
        }

        private void OpenDataResultViewController(string filePath)
        {
            NavigationController.PushViewController(new ViewControllerDataResult(filePath), true);
        }

        private void OpenTextResultViewController(string filePath)
        {
            NavigationController.PushViewController(new ViewControllerTextResult(filePath), true);
        }

        private void ProceedBasedOnCurrentSelection(string filePath)
        {
            switch (selectedOption)
            {
                case ItemType.PDFGenerating:
                    GeneratePDF(filePath);
                    break;
                case ItemType.DataExtraction:
                    OpenDataResultViewController(filePath);
                    break;
                case ItemType.TextRecognition:
                    OpenTextResultViewController(filePath);
                    break;
                default:
                    Console.WriteLine("Select an input option first");
                    break;
            }
        }

        [Export("documentInteractionControllerViewControllerForPreview:")]
        public UIViewController ViewControllerForPreview(UIDocumentInteractionController controller)
        {
            return this;
        }
    }

    public class MyTableViewSource : UITableViewSource
    {
        public event EventHandler<ListItem> ListItemClicked;

        private string cellReuseIdentifier = "cell";
        private List<ListItem> listItems = new List<ListItem>
        {
            new ListItem { Title = "title_document_scan".Localized(), Icon = "icons8-scanner-50.png", Subtitle = "subtitle_document_scan".Localized(), ItemType = ItemType.DocumentScan },
            new ListItem { Title = "title_data_extraction".Localized(), Icon = "icons8-document-50.png", Subtitle = "subtitle_data_extraction".Localized(), ItemType = ItemType.DataExtraction },
            new ListItem { Title = "title_text_recognition".Localized(), Icon = "icons8-text-box-50.png", Subtitle = "subtitle_text_recognition".Localized(), ItemType = ItemType.TextRecognition },
            new ListItem { Title = "title_PDF_generating".Localized(), Icon = "icons8-pdf-50.png", Subtitle = "subtitle_PDF_generating".Localized(), ItemType = ItemType.PDFGenerating },
            new ListItem { Title = "title_settings".Localized(), Icon = "icons8-settings-50.png", Subtitle = "subtitle_settings".Localized(), ItemType = ItemType.Settings }

        };

        public MyTableViewSource() { }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            tableView.DeselectRow(indexPath, true);
            ListItem selectedItem = listItems[indexPath.Row];
            ListItemClicked?.Invoke(this, selectedItem);
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return listItems.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (TableViewCell)tableView.DequeueReusableCell(cellReuseIdentifier, indexPath);
            var item = listItems[indexPath.Row];

            UIImage icon = UIImage.FromBundle(item.Icon).ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
            cell.Icon.Image = icon;
            cell.Title.Text = item.Title;
            cell.Subtitle.Text = item.Subtitle;

            return cell;
        }
    }
}
