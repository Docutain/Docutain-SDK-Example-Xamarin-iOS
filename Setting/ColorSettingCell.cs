using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public class ColorSettingCell : UITableViewCell, IUIColorPickerViewControllerDelegate, ISettingCell
    {
        private ColorSetting _option;
        private bool _darkColorSelected = false;

        private UILabel _title = new UILabel
        {
            Font = UIFont.BoldSystemFontOfSize(17),
            Lines = 1,
            AdjustsFontSizeToFitWidth = true,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        private UILabel _subtitle = new UILabel
        {
            Font = UIFont.SystemFontOfSize(15),
            Lines = 0,
            LineBreakMode = UILineBreakMode.WordWrap,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        private readonly UIButton _lightColorButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };
        private readonly UILabel _lightColorLabel = new UILabel { Text = "Light", Font = UIFont.SystemFontOfSize(14), TranslatesAutoresizingMaskIntoConstraints = false };
        private readonly UIButton _darkColorButton = new UIButton { TranslatesAutoresizingMaskIntoConstraints = false };
        private readonly UILabel _darkColorLabel = new UILabel { Text = "Dark", Font = UIFont.SystemFontOfSize(14), TranslatesAutoresizingMaskIntoConstraints = false };

        private const float Padding = 12.0f;
        private const float ColorSpacing = -24.0f;
        private const float ColorTitleSpacing = -36.0f;
        private const float ColorSubtitleSpacing = 8.0f;

        public ColorSettingCell(IntPtr intPtr)
        {
            InitUI();
        }

        public ColorSettingCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
        {
            InitUI();
        }

        private void InitUI()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            _darkColorButton.TouchUpInside += ButtonClicked;
            _lightColorButton.TouchUpInside += ButtonClicked;

            var colorPickerView = new UIView { TranslatesAutoresizingMaskIntoConstraints = false };
            colorPickerView.AddSubview(_lightColorButton);
            colorPickerView.AddSubview(_darkColorButton);
            colorPickerView.AddSubview(_lightColorLabel);
            colorPickerView.AddSubview(_darkColorLabel);

            _darkColorButton.TopAnchor.ConstraintEqualTo(colorPickerView.TopAnchor).Active = true;
            _darkColorButton.WidthAnchor.ConstraintEqualTo(36).Active = true;
            _darkColorButton.HeightAnchor.ConstraintEqualTo(36).Active = true;
            _darkColorButton.TrailingAnchor.ConstraintEqualTo(colorPickerView.TrailingAnchor).Active = true;
            _darkColorButton.Layer.CornerRadius = 18;
            _darkColorButton.Layer.BorderWidth = 1;
            _darkColorButton.Layer.BorderColor = (UIDevice.CurrentDevice.CheckSystemVersion(13, 0)) ? UIColor.SecondaryLabel.CGColor : UIColor.Gray.CGColor;

            _darkColorLabel.CenterXAnchor.ConstraintEqualTo(_darkColorButton.CenterXAnchor).Active = true;
            _darkColorLabel.TopAnchor.ConstraintEqualTo(_darkColorButton.BottomAnchor, ColorSubtitleSpacing).Active = true;

            _lightColorButton.TopAnchor.ConstraintEqualTo(colorPickerView.TopAnchor).Active = true;
            _lightColorButton.WidthAnchor.ConstraintEqualTo(36).Active = true;
            _lightColorButton.HeightAnchor.ConstraintEqualTo(36).Active = true;
            _lightColorButton.TrailingAnchor.ConstraintEqualTo(_darkColorButton.LeadingAnchor, ColorSpacing).Active = true;
            _lightColorButton.Layer.CornerRadius = 18;
            _lightColorButton.Layer.BorderWidth = 1;
            _lightColorButton.Layer.BorderColor = (UIDevice.CurrentDevice.CheckSystemVersion(13, 0)) ? UIColor.SecondaryLabel.CGColor : UIColor.Gray.CGColor;

            _lightColorLabel.CenterXAnchor.ConstraintEqualTo(_lightColorButton.CenterXAnchor).Active = true;
            _lightColorLabel.TopAnchor.ConstraintEqualTo(_lightColorButton.BottomAnchor, ColorSubtitleSpacing).Active = true;

            ContentView.AddSubview(colorPickerView);
            ContentView.AddSubview(_title);
            ContentView.AddSubview(_subtitle);

            colorPickerView.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            colorPickerView.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -Padding).Active = true;
            colorPickerView.HeightAnchor.ConstraintEqualTo(64).Active = true;
            colorPickerView.WidthAnchor.ConstraintEqualTo(84).Active = true;

            _title.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, Padding).Active = true;
            _title.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, Padding).Active = true;
            _title.TrailingAnchor.ConstraintEqualTo(colorPickerView.LeadingAnchor, ColorTitleSpacing).Active = true;

            _subtitle.TopAnchor.ConstraintEqualTo(_title.BottomAnchor).Active = true;
            _subtitle.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -Padding).Active = true;
            _subtitle.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, Padding).Active = true;
            _subtitle.TrailingAnchor.ConstraintEqualTo(colorPickerView.LeadingAnchor, ColorTitleSpacing).Active = true;
        }

        public void ButtonClicked(object sender, EventArgs args)
        {
            _darkColorSelected = sender == _darkColorButton;
            if (UIDevice.CurrentDevice.CheckSystemVersion(14, 0))
            {
                var colorPicker = new UIColorPickerViewController();
                colorPicker.Title = _darkColorSelected ? "Pick Dark Color" : "Pick Light Color";
                colorPicker.SupportsAlpha = false;
                colorPicker.Delegate = this;
                colorPicker.ModalPresentationStyle = UIModalPresentationStyle.Popover;
                colorPicker.PopoverPresentationController.SourceView = sender as UIButton;
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(colorPicker, true, null);
            }
            else
            {
                var alert = UIAlertController.Create("Info", "UIColorPickerViewController is not supported for iOS versions below 14. If you want to present a color picker to your users using iOS versions below 14, you need to implement your own color picker.", UIAlertControllerStyle.Alert);
                alert.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
                UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
            }
        }

        public void Configure(ISetting option)
        {
            if (!(option is ColorSetting colorOption))
                throw new InvalidOperationException("Wrong option was used for configuring the cell");

            _title.Text = option.Title;
            _subtitle.Text = option.Subtitle;
            _darkColorButton.BackgroundColor = colorOption.ColorDark;
            _lightColorButton.BackgroundColor = colorOption.ColorLight;
            _option = colorOption;
        }

        [Foundation.Export("colorPickerViewControllerDidFinish:")]
        public void DidFinish(UIColorPickerViewController viewController)
        {
            viewController.DismissViewController(true, null);
        }

        [Foundation.Export("colorPickerViewControllerDidSelectColor:")]
        public void DidSelectColor(UIColorPickerViewController viewController)
        {
            if (_darkColorSelected)
            {
                _option.ColorDark = viewController.SelectedColor;
                _darkColorButton.BackgroundColor = viewController.SelectedColor;
            }
            else
            {
                _option.ColorLight = viewController.SelectedColor;
                _lightColorButton.BackgroundColor = viewController.SelectedColor;
            }
        }

        [Foundation.Export("colorPickerViewController:didSelectColor:continuously:")]
        public void DidSelectColorContinuously(UIColorPickerViewController viewController, UIColor color, bool continuously)
        {
            if (continuously)
                return;

            if (_darkColorSelected)
            {
                _option.ColorDark = viewController.SelectedColor;
                _darkColorButton.BackgroundColor = viewController.SelectedColor;
            }
            else
            {
                _option.ColorLight = viewController.SelectedColor;
                _lightColorButton.BackgroundColor = viewController.SelectedColor;
            }
        }
    }

}