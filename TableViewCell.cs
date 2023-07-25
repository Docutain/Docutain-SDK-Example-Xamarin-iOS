using Foundation;
using System;
using UIKit;

public class TableViewCell : UITableViewCell
{
    public UILabel Title { get; set; }
    public UILabel Subtitle { get; set; }

    public UIImageView Icon { get; set; }

    private nfloat padding = 16.0f;

    public TableViewCell(IntPtr ptr) : base(ptr)
    {
        Init();
    }

    public TableViewCell(UITableViewCellStyle style, string reuseIdentifier) : base(style, reuseIdentifier)
    {
        Init();
    }

    private void Init()
    {
        Title = new UILabel
        {
            Font = UIFont.BoldSystemFontOfSize(17),
            Lines = 0,
            LineBreakMode = UILineBreakMode.WordWrap,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        Subtitle = new UILabel
        {
            Font = UIFont.SystemFontOfSize(16),
            TextColor = UIDevice.CurrentDevice.CheckSystemVersion(13, 0) ? UIColor.SecondaryLabel : UIColor.Black,
            Lines = 0,
            LineBreakMode = UILineBreakMode.WordWrap,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        Icon = new UIImageView
        {
            ContentMode = UIViewContentMode.ScaleAspectFit,
            TintColor = UIColor.SystemGray,
            TranslatesAutoresizingMaskIntoConstraints = false
        };

        ContentView.AddSubview(Icon);
        ContentView.AddSubview(Title);
        ContentView.AddSubview(Subtitle);

        Icon.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, padding).Active = true;
        Icon.LeadingAnchor.ConstraintEqualTo(ContentView.LeadingAnchor, padding).Active = true;
        Icon.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -padding).Active = true;
        Icon.WidthAnchor.ConstraintEqualTo(42).Active = true;

        Title.TopAnchor.ConstraintEqualTo(ContentView.TopAnchor, padding).Active = true;
        Title.LeadingAnchor.ConstraintEqualTo(Icon.TrailingAnchor, padding).Active = true;
        Title.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;

        Subtitle.TopAnchor.ConstraintEqualTo(Title.BottomAnchor).Active = true;
        Subtitle.BottomAnchor.ConstraintEqualTo(ContentView.BottomAnchor, -padding).Active = true;
        Subtitle.LeadingAnchor.ConstraintEqualTo(Icon.TrailingAnchor, padding).Active = true;
        Subtitle.TrailingAnchor.ConstraintEqualTo(ContentView.TrailingAnchor).Active = true;
    }
}
