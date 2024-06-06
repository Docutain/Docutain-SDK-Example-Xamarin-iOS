using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{

    public class MyCustomHeader : UITableViewHeaderFooterView
    {
        public UILabel Title { get; }

        public MyCustomHeader(string reuseIdentifier) : base(new NSString(reuseIdentifier))
        {
            Title = new UILabel();
            ConfigureContents();
        }

        public MyCustomHeader(IntPtr handle) : base(handle)
        {
            Title = new UILabel();
            ConfigureContents();
        }

        private void ConfigureContents()
        {
            Title.TranslatesAutoresizingMaskIntoConstraints = false;
            ContentView.AddSubview(Title);
            NSLayoutConstraint.ActivateConstraints(new[]
            {
            // Center the label vertically, and use it to fill the remaining
            // space in the header view.
            Title.HeightAnchor.ConstraintEqualTo(30),
            Title.LeadingAnchor.ConstraintEqualTo(ContentView.LayoutMarginsGuide.LeadingAnchor),
            Title.TrailingAnchor.ConstraintEqualTo(ContentView.LayoutMarginsGuide.TrailingAnchor),
            Title.CenterYAnchor.ConstraintEqualTo(ContentView.CenterYAnchor)
        });
        }
    }

}