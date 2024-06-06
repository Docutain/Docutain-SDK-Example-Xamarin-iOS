using Docutain.SDK.Xamarin.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public class MyUITableViewDelegate : UITableViewDelegate
    {
        public MyUITableViewDelegate()
        {
        }

        public MyUITableViewDelegate(IntPtr handle) : base(handle)
        {

        }

        public override void WillDisplayHeaderView(UITableView tableView, UIView headerView, nint section)
        {
            if (headerView is UITableViewHeaderFooterView header)
            {
                header.TextLabel.TextColor = UIColor.FromName("AccentColor");
            }
        }
    }

    public class MyUITableViewDataSource : UITableViewDataSource
    {
        private string[] headers = { "Color Settings", "Scan Settings", "Edit Settings" };
        private List<List<ISetting>> settingsItems = new List<List<ISetting>>();

        public MyUITableViewDataSource()
        {
            InitData();

        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return settingsItems[(int)section].Count;
        }

        public override string TitleForHeader(UITableView tableView, nint section)
        {
            return headers[section];
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {

            var currentOption = settingsItems[indexPath.Section][indexPath.Row];

            var cellType = CellTypeFor(currentOption.GetType());

            var cell = tableView.DequeueReusableCell(cellType.ToString(), indexPath);

            if (cell is ISettingCell settingCell)
            {
                settingCell.Configure(currentOption);
            }

            return cell;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return headers.Length;
        }
        private static Type CellTypeFor(Type modelType)
        {
            if (registeredTypes.TryGetValue(modelType, out Type cellType))
            {
                return cellType;
            }
            return null;
        }

        private void InitData()
        {
            settingsItems = new List<List<ISetting>>
            {
                new List<ISetting>
                {
                    new ColorSetting(SettingType.ColorPrimary, DocutainPreferences.ColorPrimary.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorSecondary, DocutainPreferences.ColorSecondary.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorOnSecondary, DocutainPreferences.ColorOnSecondary.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorScanButtonsLayoutBackground, DocutainPreferences.ColorScanButtonsLayoutBackground.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorScanButtonsForeground, DocutainPreferences.ColorScanButtonsForeground.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorScanPolygon, DocutainPreferences.ColorScanPolygon.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorBottomBarBackground, DocutainPreferences.ColorBottomBarBackground.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorBottomBarForeground, DocutainPreferences.ColorBottomBarForeground.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorTopBarBackground, DocutainPreferences.ColorTopBarBackground.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type)),
                    new ColorSetting(SettingType.ColorTopBarForeground, DocutainPreferences.ColorTopBarForeground.ToStringDict(), (colorLight, colorDark, type) => ColorHandler(colorLight, colorDark, type))
                },
                new List<ISetting>
                {
                    new BoolSetting(SettingType.AllowCaptureModeSetting, DocutainPreferences.AllowCaptureModeSetting, (value, type) => BoolHandler(value, type)),
                    new BoolSetting(SettingType.AutoCapture, DocutainPreferences.AutoCapture, (value, type) => BoolHandler(value, type)),
                    new BoolSetting(SettingType.AutoCrop, DocutainPreferences.AutoCrop, (value, type) => BoolHandler(value, type)),
                    new BoolSetting(SettingType.MultiPage, DocutainPreferences.MultiPage, (value, type) => BoolHandler(value, type)),
                    new PickerSetting(SettingType.DefaultScanFilter, DocutainPreferences.DefaultScanFilter, new List<ScanFilter> { ScanFilter.Auto, ScanFilter.Gray, ScanFilter.BlackWhite, ScanFilter.Original, ScanFilter.Text, ScanFilter.Auto2, ScanFilter.Illustration }, FilterHandler)
                },
                new List<ISetting>
                {
                    new BoolSetting(SettingType.AllowPageFilter, DocutainPreferences.AllowPageFilter, (value, type) => BoolHandler(value, type)),
                    new BoolSetting(SettingType.AllowPageRotation, DocutainPreferences.AllowPageRotation, (value, type) => BoolHandler(value, type)),
                    new BoolSetting(SettingType.AllowPageArrangement, DocutainPreferences.AllowPageArrangement, (value, type) => BoolHandler(value, type)),
                    new BoolSetting(SettingType.AllowPageCropping, DocutainPreferences.AllowPageCropping, (value, type) => BoolHandler(value, type)),
                    new BoolSetting(SettingType.PageArrangementShowDeleteButton, DocutainPreferences.PageArrangementShowDeleteButton, (value, type) => BoolHandler(value, type)),
                    new BoolSetting(SettingType.PageArrangementShowPageNumber, DocutainPreferences.PageArrangementShowPageNumber, (value, type) => BoolHandler(value, type))
                }
            };
        }

        private void BoolHandler(bool value, SettingType settingsType)
        {
            DocutainPreferences.SetBoolValue(value, settingsType);
        }

        private void FilterHandler(ScanFilter filter, SettingType settingsType)
        {
            DocutainPreferences.SetFilterValue(filter);
        }

        private void ColorHandler(UIColor colorLight, UIColor colorDark, SettingType settingsType)
        {
            DocutainPreferences.SetColorValues(colorLight, colorDark, settingsType);
        }

        private static Dictionary<Type, Type> registeredTypes = new Dictionary<Type, Type>
        {
            { typeof(BoolSetting), typeof(BoolSettingCell) },
            { typeof(PickerSetting), typeof(PickerSettingCell) },
            { typeof(ColorSetting), typeof(ColorSettingCell) }
        };
    }

    public class ViewControllerSettings : UIViewController
    {

        private UITableView tableView = new UITableView();
        private UIButton resetButton = new UIButton();


        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            NavigationItem.BackButtonTitle = "";
            Title = "Settings".Localized();

            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                View.BackgroundColor = UIColor.SystemBackground;
            }
            else
            {
                View.BackgroundColor = UIColor.White;
            }

            var resetButton = new UIButton(UIButtonType.System);
            resetButton.SetTitle("Reset", UIControlState.Normal);
            resetButton.Font = UIFont.BoldSystemFontOfSize(18);
            if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
            {
                resetButton.BackgroundColor = UIColor.SystemFill;
            }
            else
            {
                resetButton.BackgroundColor = UIColor.FromRGB(224, 224, 224);
            }
            resetButton.ContentEdgeInsets = new UIEdgeInsets(12, 12, 12, 12);
            resetButton.Layer.CornerRadius = 8;
            resetButton.TouchUpInside += ResetButtonTapped;
            View.AddSubview(resetButton);
            resetButton.TranslatesAutoresizingMaskIntoConstraints = false;
            resetButton.BottomAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.BottomAnchor, UIApplication.SharedApplication.KeyWindow?.SafeAreaInsets.Bottom > 0 ? 0 : -12).Active = true;
            resetButton.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor, 12).Active = true;
            resetButton.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor, -12).Active = true;

            View.AddSubview(tableView);
            tableView.TranslatesAutoresizingMaskIntoConstraints = false;
            tableView.TopAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TopAnchor).Active = true;
            tableView.BottomAnchor.ConstraintEqualTo(resetButton.TopAnchor, -12).Active = true;
            tableView.LeadingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.LeadingAnchor).Active = true;
            tableView.TrailingAnchor.ConstraintEqualTo(View.SafeAreaLayoutGuide.TrailingAnchor).Active = true;


            tableView.RegisterClassForCellReuse(typeof(BoolSettingCell), typeof(BoolSettingCell).ToString());
            tableView.RegisterClassForCellReuse(typeof(PickerSettingCell), typeof(PickerSettingCell).ToString());
            tableView.RegisterClassForCellReuse(typeof(ColorSettingCell), typeof(ColorSettingCell).ToString());

            //tableView.RegisterClassForHeaderFooterViewReuse(typeof(MyCustomHeader), typeof(MyCustomHeader).ToString());

            tableView.Delegate = new MyUITableViewDelegate();
            tableView.DataSource = new MyUITableViewDataSource();
        }

        private void ResetButtonTapped(object sender, EventArgs e)
        {
            DocutainPreferences.Reset();
            tableView.DataSource = new MyUITableViewDataSource();
            tableView.ReloadData();
        }
    }
}