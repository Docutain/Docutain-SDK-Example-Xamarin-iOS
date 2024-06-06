using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{

    public class BoolSettingCell : UITableViewCell, ISettingCell
    {
        private BoolSetting _option;

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

        private readonly UISwitch _toggle = new UISwitch
        {
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        private const float Padding = 12.0f;
        private const float SwitchSpacing = -24.0f;

        public BoolSettingCell(IntPtr intPtr)
        {
            InitUI();
        }

        public BoolSettingCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
        {
            InitUI();
        }

        private void InitUI()
        {
            SelectionStyle = UITableViewCellSelectionStyle.None;

            _toggle.ValueChanged += ValueChanged;

            ContentView.AddSubview(_toggle);
            ContentView.AddSubview(_title);
            ContentView.AddSubview(_subtitle);

            _toggle.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor).Active = true;
            _toggle.WidthAnchor.ConstraintEqualTo(58).Active = true;
            _toggle.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor, -Padding).Active = true;

            _title.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, Padding).Active = true;
            _title.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, Padding).Active = true;
            _title.TrailingAnchor.ConstraintEqualTo(_toggle.LeadingAnchor, SwitchSpacing).Active = true;

            _subtitle.TopAnchor.ConstraintEqualTo(_title.BottomAnchor).Active = true;
            _subtitle.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -Padding).Active = true;
            _subtitle.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, Padding).Active = true;
            _subtitle.TrailingAnchor.ConstraintEqualTo(_toggle.LeadingAnchor, SwitchSpacing).Active = true;
        }

        [Foundation.Export("initWithCoder:")]
        public BoolSettingCell(NSCoder coder) : base(coder)
        {
            // Not implemented
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            _option.Value = _toggle.On;//Null check ? entfernt
        }

        public void Configure(ISetting option)
        {
            if (!(option is BoolSetting boolOption))
                throw new InvalidOperationException("Wrong option was used for configuring the cell");

            _title.Text = option.Title;
            _subtitle.Text = option.Subtitle;
            _toggle.On = boolOption.Value;
            _option = boolOption;
        }
    }

}