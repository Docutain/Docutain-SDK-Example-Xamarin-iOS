using Foundation;
using UIKit;
//using DocutainSdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreGraphics;
using System.Security.Policy;
using CoreFoundation;
using static AVFoundation.AVMetadataIdentifiers;
using System.Text.Json;
using System.Text.RegularExpressions;
using Docutain.SDK.Xamarin.iOS;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public class ViewControllerDataResult : UIViewController
    {
        private UIActivityIndicatorView loadingIndicator = new UIActivityIndicatorView();
        private UITextView textFieldName1 = GetTextView();
        private UITextView textFieldName2 = GetTextView();
        private UITextView textFieldName3 = GetTextView();
        private UITextView textFieldZipcode = GetTextView();
        private UITextView textFieldCity = GetTextView();
        private UITextView textFieldStreet = GetTextView();
        private UITextView textFieldPhone = GetTextView();
        private UITextView textFieldCustomer = GetTextView();
        private UITextView textFieldIBAN = GetTextView();
        private UITextView textFieldBIC = GetTextView();
        private UITextView textFieldDate = GetTextView();
        private UITextView textFieldAmount = GetTextView();
        private UITextView textFieldInvoiceID = GetTextView();
        private UITextView textFieldReference = GetTextView();
        private UITextView textFieldPaymentState = GetTextView();

        private UILabel labelName1, labelName2, labelName3, labelZipcode, labelCity, labelStreet, labelIBAN, labelPhone,
                        labelCustomer, labelBIC, labelDate, labelAmount, labelInvoiceID, labelReference, labelPaymentState;
        private UIStackView stackView;
        private UIScrollView scrollview = new UIScrollView();
        private NSLayoutConstraint stackViewWidthConstraint;
        private nfloat padding = 16.0f;
        private string filePath;

        public ViewControllerDataResult(string filePath)
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

            scrollview.TranslatesAutoresizingMaskIntoConstraints = false;
            View.AddSubview(scrollview);

            NSLayoutConstraint.ActivateConstraints(new[]
            {
                scrollview.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor),
                scrollview.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor),
                scrollview.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor),
                scrollview.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor)
            });

            labelName1 = GetHeaderLabel();
            labelName2 = GetHeaderLabel();
            labelName3 = GetHeaderLabel();
            labelZipcode = GetHeaderLabel();
            labelCity = GetHeaderLabel();
            labelStreet = GetHeaderLabel();
            labelIBAN = GetHeaderLabel();
            labelPhone = GetHeaderLabel();
            labelCustomer = GetHeaderLabel();
            labelBIC = GetHeaderLabel();
            labelDate = GetHeaderLabel();
            labelAmount = GetHeaderLabel();
            labelInvoiceID = GetHeaderLabel();
            labelReference = GetHeaderLabel();
            labelPaymentState = GetHeaderLabel();

            labelName1.Text = "Name1".Localized();
            labelName2.Text = "Name2".Localized();
            labelName3.Text = "Name3".Localized();
            labelZipcode.Text = "ZipCode".Localized();
            labelCity.Text = "City".Localized();
            labelStreet.Text = "Street".Localized();
            labelPhone.Text = "Phone".Localized();
            labelCustomer.Text = "CustomerID".Localized();
            labelIBAN.Text = "IBAN".Localized();
            labelBIC.Text = "BIC".Localized();
            labelDate.Text = "Date".Localized();
            labelAmount.Text = "Amount".Localized();
            labelInvoiceID.Text = "InvoiceID".Localized();
            labelReference.Text = "Reference".Localized();
            labelPaymentState.Text = "PaymentState".Localized();

            stackView = new UIStackView();

            stackView.Axis = UILayoutConstraintAxis.Vertical;
            stackView.Spacing = 4;
            stackView.AddArrangedSubview(labelName1);
            stackView.AddArrangedSubview(textFieldName1);
            stackView.AddArrangedSubview(labelName2);
            stackView.AddArrangedSubview(textFieldName2);
            stackView.AddArrangedSubview(labelName3);
            stackView.AddArrangedSubview(textFieldName3);
            stackView.AddArrangedSubview(labelZipcode);
            stackView.AddArrangedSubview(textFieldZipcode);
            stackView.AddArrangedSubview(labelCity);
            stackView.AddArrangedSubview(textFieldCity);
            stackView.AddArrangedSubview(labelStreet);
            stackView.AddArrangedSubview(textFieldStreet);
            stackView.AddArrangedSubview(labelCustomer);
            stackView.AddArrangedSubview(textFieldCustomer);
            stackView.AddArrangedSubview(labelIBAN);
            stackView.AddArrangedSubview(textFieldIBAN);
            stackView.AddArrangedSubview(labelBIC);
            stackView.AddArrangedSubview(textFieldBIC);
            stackView.AddArrangedSubview(labelDate);
            stackView.AddArrangedSubview(textFieldDate);
            stackView.AddArrangedSubview(labelAmount);
            stackView.AddArrangedSubview(textFieldAmount);
            stackView.AddArrangedSubview(labelInvoiceID);
            stackView.AddArrangedSubview(textFieldInvoiceID);
            stackView.AddArrangedSubview(labelReference);
            stackView.AddArrangedSubview(textFieldReference);
            stackView.AddArrangedSubview(labelPaymentState);
            stackView.AddArrangedSubview(textFieldPaymentState);
            stackView.TranslatesAutoresizingMaskIntoConstraints = false;
            scrollview.AddSubview(stackView);
            NSLayoutConstraint.ActivateConstraints(new NSLayoutConstraint[]
            {
                stackView.TopAnchor.ConstraintEqualTo(scrollview.TopAnchor, padding),
                stackView.LeadingAnchor.ConstraintEqualTo(scrollview.LeadingAnchor, padding),
                stackView.TrailingAnchor.ConstraintEqualTo(scrollview.TrailingAnchor, -padding),
                stackView.BottomAnchor.ConstraintEqualTo(scrollview.BottomAnchor, -padding)
            });
            stackViewWidthConstraint = stackView.WidthAnchor.ConstraintGreaterThanOrEqualTo(0.0f);
            stackViewWidthConstraint.Active = true;

            loadingIndicator.HidesWhenStopped = true;
            loadingIndicator.StartAnimating();
            View.AddSubview(loadingIndicator);
            loadingIndicator.TranslatesAutoresizingMaskIntoConstraints = false;
            loadingIndicator.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
            loadingIndicator.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor).Active = true;

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
                LoadData();
            else
                Console.WriteLine("Data Extraction is only available for iOS 13+");
        }       

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            stackViewWidthConstraint.Constant = View.SafeAreaLayoutGuide.LayoutFrame.Width - 2 * padding;
        }

        private void LoadData()
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

                //analyze the currently loaded document and get the detected data
                string analyzeData = DocumentDataReader.Analyze();
                if (string.IsNullOrEmpty(analyzeData))
                {
                    Console.WriteLine("no data detected");
                    return;
                }

                try
                {                   
                    //detected data is returned as JSON, so deserializing the data to extract the key-value pairs
                    //see https://docs.docutain.com/docs/Xamarin/dataExtraction for more information
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Dictionary<string, object> jsonArray = JsonSerializer.Deserialize<Dictionary<string, object>>(analyzeData, options);

                    Dictionary<string, object> address = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonArray["Address"].ToString(), options);
                    
                    var name1 = address.TryGetValue("Name1", out var name1Obj) ? name1Obj.ToString() : "";
                    var name2 = address.TryGetValue("Name2", out var name2Obj) ? name2Obj.ToString() : "";
                    var name3 = address.TryGetValue("Name3", out var name3Obj) ? name3Obj.ToString() : "";
                    var zipcode = address.TryGetValue("Zipcode", out var zipcodeObj) ? zipcodeObj.ToString() : "";
                    var city = address.TryGetValue("City", out var cityObj) ? cityObj.ToString() : "";
                    var street = address.TryGetValue("Street", out var streetObj) ? streetObj.ToString() : "";
                    var phone = address.TryGetValue("Phone", out var phoneObj) ? phoneObj.ToString() : "";
                    var customerId = address.TryGetValue("CustomerId", out var customerIdObj) ? customerIdObj.ToString() : "";


                    Dictionary<string, object> bank = new Dictionary<string, object>();

                    if (jsonArray.ContainsKey("Bank"))
                        bank = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonArray["Bank"].ToString(), options);

                    var IBAN = "";
                    var BIC = "";
                    if (bank.Count > 0)
                    {
                        IBAN = bank.TryGetValue("IBAN", out var IBANObj) ? IBANObj.ToString() : "";
                        BIC = bank.TryGetValue("BIC", out var BICObj) ? BICObj.ToString() : "";
                    }

                    var date = jsonArray.TryGetValue("Date", out var dateObj) ? dateObj.ToString() : "";
                    var amount = jsonArray.TryGetValue("Amount", out var amountObj) ? amountObj.ToString() : "";
                    var invoiceId = jsonArray.TryGetValue("InvoiceId", out var invoiceIdObj) ? invoiceIdObj.ToString() : "";
                    var reference = jsonArray.TryGetValue("Reference", out var referenceObj) ? referenceObj.ToString() : "";
                    var paid = jsonArray.TryGetValue("PaymentState", out var paidObj) ? paidObj.ToString() : "";

                    DispatchQueue.MainQueue.DispatchAsync(() =>
                    {
                        loadingIndicator.StopAnimating();
                        textFieldName1.Text = name1;
                        textFieldName1.Hidden = string.IsNullOrEmpty(name1);
                        labelName1.Hidden = string.IsNullOrEmpty(name1);
                        textFieldName2.Text = name2;
                        textFieldName2.Hidden = string.IsNullOrEmpty(name2);
                        labelName2.Hidden = string.IsNullOrEmpty(name2);
                        textFieldName3.Text = name3;
                        textFieldName3.Hidden = string.IsNullOrEmpty(name3);
                        labelName3.Hidden = string.IsNullOrEmpty(name3);
                        textFieldZipcode.Text = zipcode;
                        textFieldZipcode.Hidden = string.IsNullOrEmpty(zipcode);
                        labelZipcode.Hidden = string.IsNullOrEmpty(zipcode);
                        textFieldCity.Text = city;
                        textFieldCity.Hidden = string.IsNullOrEmpty(city);
                        labelCity.Hidden = string.IsNullOrEmpty(city);
                        textFieldStreet.Text = street;
                        textFieldStreet.Hidden = string.IsNullOrEmpty(street);
                        labelStreet.Hidden = string.IsNullOrEmpty(street);
                        textFieldPhone.Text = phone;
                        textFieldPhone.Hidden = string.IsNullOrEmpty(phone);
                        labelPhone.Hidden = string.IsNullOrEmpty(phone);
                        textFieldCustomer.Text = customerId;
                        textFieldCustomer.Hidden = string.IsNullOrEmpty(customerId);
                        labelCustomer.Hidden = string.IsNullOrEmpty(customerId);

                        var IBANRegex = new Regex(".{4}");
                        IBAN = IBANRegex.Replace(IBAN, "$0 ");
                        textFieldIBAN.Text = IBAN;
                        textFieldIBAN.Hidden = string.IsNullOrEmpty(IBAN);
                        labelIBAN.Hidden = string.IsNullOrEmpty(IBAN);
                        textFieldBIC.Text = BIC;
                        textFieldBIC.Hidden = string.IsNullOrEmpty(BIC);
                        labelBIC.Hidden = string.IsNullOrEmpty(BIC);
                        textFieldDate.Text = date;
                        textFieldDate.Hidden = string.IsNullOrEmpty(date);
                        labelDate.Hidden = string.IsNullOrEmpty(date);
                        textFieldAmount.Text = amount;
                        textFieldAmount.Hidden = string.IsNullOrEmpty(amount) || amount == "0.00";
                        labelAmount.Hidden = string.IsNullOrEmpty(amount) || amount == "0.00";
                        textFieldInvoiceID.Text = invoiceId;
                        textFieldInvoiceID.Hidden = string.IsNullOrEmpty(invoiceId);
                        labelInvoiceID.Hidden = string.IsNullOrEmpty(invoiceId);
                        textFieldReference.Text = reference;
                        textFieldReference.Hidden = string.IsNullOrEmpty(reference);
                        labelReference.Hidden = string.IsNullOrEmpty(reference);
                        textFieldPaymentState.Text = paid;
                        textFieldPaymentState.Hidden = string.IsNullOrEmpty(paid);
                        labelPaymentState.Hidden = string.IsNullOrEmpty(paid);
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            });
        }

        private UILabel GetHeaderLabel()
        {
            var label = new UILabel();
            label.Font = UIFont.SystemFontOfSize(14);
            label.TextColor = UIColor.SystemGray;
            label.Hidden = true;
            return label;
        }

        private static UITextView GetTextView()
        {
            var textView = new UITextView();
            textView.ScrollEnabled = false;
            textView.TextContainerInset = new UIEdgeInsets(8, 8, 8, 8);
            textView.Editable = false;
            textView.Font = UIFont.SystemFontOfSize(16);
            textView.Layer.BorderColor = UIColor.SystemGray.CGColor;
            textView.Layer.BorderWidth = 1;
            textView.Layer.CornerRadius = 8;
            textView.Hidden = true;
            return textView;
        }
    }
}


