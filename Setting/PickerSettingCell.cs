using Docutain.SDK.Xamarin.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using static Xamarin.Essentials.AppleSignInAuthenticator;

namespace Docutain_SDK_Example_Xamarin_iOS
{

    public class MyPickerViewModel : UIPickerViewModel
    {
        public PickerSetting option;

        public List<ScanFilter> pickerItems = new List<ScanFilter>();


        public MyPickerViewModel() { }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return pickerItems[(int)row].ToString();
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            option.Value = pickerItems[(int)row];
        }

        public override nint GetComponentCount(UIPickerView pickerView) => 1;

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component) => pickerItems.Count;

    }


    public class PickerSettingCell : UITableViewCell, ISettingCell
    {
        private  UILabel title = new UILabel();
        private  UILabel subtitle = new UILabel();
        private readonly UIPickerView picker = new UIPickerView();

        private const float padding = 12.0f;
        private const float pickerSpacing = -8.0f;

        public PickerSettingCell(IntPtr intPtr)
        {
            InitUI();
        }

        public PickerSettingCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
        {
            InitUI();
        }

        private void InitUI()
        {
            title = new UILabel
            {
                Font = UIFont.BoldSystemFontOfSize(17),
                Lines = 1,
                AdjustsFontSizeToFitWidth = true,
                Text = "Placeholder"
            };

            subtitle = new UILabel
            {
                Font = UIFont.SystemFontOfSize(15),
                Lines = 0,
                LineBreakMode = UILineBreakMode.WordWrap,
                Text = "Placeholder"
            };
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                subtitle.TextColor = UIColor.SecondaryLabel;
            }

            SelectionStyle = UITableViewCellSelectionStyle.None;

            ContentView.AddSubview(picker);
            ContentView.AddSubview(title);
            ContentView.AddSubview(subtitle);

            picker.TranslatesAutoresizingMaskIntoConstraints = false;
            picker.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            picker.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
            picker.WidthAnchor.ConstraintEqualTo(184).Active = true;
            picker.HeightAnchor.ConstraintEqualTo(84).Active = true;

            title.TranslatesAutoresizingMaskIntoConstraints = false;
            title.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, padding).Active = true;
            title.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, padding).Active = true;
            title.TrailingAnchor.ConstraintEqualTo(picker.LeadingAnchor, pickerSpacing).Active = true;

            subtitle.TranslatesAutoresizingMaskIntoConstraints = false;
            subtitle.TopAnchor.ConstraintEqualTo(title.BottomAnchor).Active = true;
            subtitle.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -padding).Active = true;
            subtitle.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, padding).Active = true;
            subtitle.TrailingAnchor.ConstraintEqualTo(picker.LeadingAnchor, pickerSpacing).Active = true;

            picker.Model = new MyPickerViewModel();
        }

        public void Configure(ISetting option)
        {
            MyPickerViewModel model = ((MyPickerViewModel)picker.Model);

            if (!(option is PickerSetting pickerOption))
            {
                throw new InvalidOperationException("Wrong option was used for configuring the cell");
            }

            title.Text = option.Title;
            subtitle.Text = option.Subtitle;
            model.option = pickerOption;
            model.pickerItems = pickerOption.Items;
            picker.ReloadAllComponents();
            picker.Select(model.pickerItems.IndexOf(pickerOption.Value), 0, false);
        }
    }
}