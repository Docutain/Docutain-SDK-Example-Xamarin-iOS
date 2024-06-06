using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public enum SettingType
    {
        AllowCaptureModeSetting,
        AutoCapture,
        AutoCrop,
        MultiPage,
        DefaultScanFilter,
        AllowPageFilter,
        AllowPageRotation,
        AllowPageArrangement,
        AllowPageCropping,
        PageArrangementShowDeleteButton,
        PageArrangementShowPageNumber,
        ColorPrimary,
        ColorSecondary,
        ColorOnSecondary,
        ColorScanButtonsLayoutBackground,
        ColorScanButtonsForeground,
        ColorScanPolygon,
        ColorBottomBarBackground,
        ColorBottomBarForeground,
        ColorTopBarBackground,
        ColorTopBarForeground
    }

    public interface ISetting
    {
        SettingType SettingType { get; }
        string Title { get; }
        string Subtitle { get; }
    }

}