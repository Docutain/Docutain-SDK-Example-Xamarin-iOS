using Docutain.SDK.Xamarin.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{   

    public static class DocutainPreferences
    {
        public static bool AllowCaptureModeSetting
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"AllowCaptureModeSetting") as NSNumber)?.BoolValue ?? false;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "AllowCaptureModeSetting");
        }

        public static bool AutoCapture
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"AutoCapture") as NSNumber)?.BoolValue ?? true;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "AutoCapture");
        }

        public static bool AutoCrop
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"AutoCrop") as NSNumber)?.BoolValue ?? true;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "AutoCrop");
        }

        public static bool MultiPage
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"MultiPage") as NSNumber)?.BoolValue ?? true;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "MultiPage");
        }

        public static ScanFilter DefaultScanFilter
        {
            get
            {
                var savedFilter = (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"DefaultScanFilter") as NSNumber)?.Int32Value ?? -1;
                return Enum.IsDefined(typeof(ScanFilter), (long)savedFilter) ? (ScanFilter)savedFilter : ScanFilter.Illustration;
            }
            set => NSUserDefaults.StandardUserDefaults.SetInt((int)value, "DefaultScanFilter");
        }

        public static bool AllowPageFilter
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"AllowPageFilter") as NSNumber)?.BoolValue ?? true;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "AllowPageFilter");
        }

        public static bool AllowPageRotation
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"AllowPageRotation") as NSNumber)?.BoolValue ?? true;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "AllowPageRotation");
        }

        public static bool AllowPageArrangement
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"AllowPageArrangement") as NSNumber)?.BoolValue ?? true;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "AllowPageArrangement");
        }

        public static bool AllowPageCropping
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"AllowPageCropping") as NSNumber)?.BoolValue ?? true;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "AllowPageCropping");
        }

        public static bool PageArrangementShowDeleteButton
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"PageArrangementShowDeleteButton") as NSNumber)?.BoolValue ?? false;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "PageArrangementShowDeleteButton");
        }

        public static bool PageArrangementShowPageNumber
        {
            get => (NSUserDefaults.StandardUserDefaults.ValueForKey((NSString)"PageArrangementShowPageNumber") as NSNumber)?.BoolValue ?? true;
            set => NSUserDefaults.StandardUserDefaults.SetBool(value, "PageArrangementShowPageNumber");
        }

        public static NSDictionary ColorPrimary
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorPrimary") ?? new NSDictionary("light", "#4caf50", "dark", "#4caf50");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorPrimary");
        }

        public static NSDictionary ColorSecondary
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorSecondary") ?? new NSDictionary("light", "#4caf50", "dark", "#4caf50");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorSecondary");
        }

        public static NSDictionary ColorOnSecondary
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorOnSecondary") ?? new NSDictionary("light", "#ffffff", "dark", "#000000");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorOnSecondary");
        }

        public static NSDictionary ColorScanButtonsLayoutBackground
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorScanButtonsLayoutBackground") ?? new NSDictionary("light", "#121212", "dark", "#121212");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorScanButtonsLayoutBackground");
        }

        public static NSDictionary ColorScanButtonsForeground
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorScanButtonsForeground") ?? new NSDictionary("light", "#ffffff", "dark", "#ffffff");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorScanButtonsForeground");
        }

        public static NSDictionary ColorScanPolygon
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorScanPolygon") ?? new NSDictionary("light", "#4caf50", "dark", "#4caf50");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorScanPolygon");
        }

        public static NSDictionary ColorBottomBarBackground
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorBottomBarBackground") ?? new NSDictionary("light", "#ffffff", "dark", "#212121");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorBottomBarBackground");
        }

        public static NSDictionary ColorBottomBarForeground
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorBottomBarForeground") ?? new NSDictionary("light", "#323232", "dark", "#bebebe");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorBottomBarForeground");
        }

        public static NSDictionary ColorTopBarBackground
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorTopBarBackground") ?? new NSDictionary("light", "#4caf50", "dark", "#2a2a2a");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorTopBarBackground");
        }

        public static NSDictionary ColorTopBarForeground
        {
            get => NSUserDefaults.StandardUserDefaults.DictionaryForKey("ColorTopBarForeground") ?? new NSDictionary("light", "#ffffff", "dark", "#ffffff");
            set => NSUserDefaults.StandardUserDefaults.SetValueForKey(value, (NSString)"ColorTopBarForeground");
        }

        public static void SetBoolValue(bool value, SettingType settingType)
        {
            switch (settingType)
            {
                case SettingType.AllowCaptureModeSetting:
                    AllowCaptureModeSetting = value;
                    break;
                case SettingType.AutoCapture:
                    AutoCapture = value;
                    break;
                case SettingType.AutoCrop:
                    AutoCrop = value;
                    break;
                case SettingType.MultiPage:
                    MultiPage = value;
                    break;
                case SettingType.AllowPageFilter:
                    AllowPageFilter = value;
                    break;
                case SettingType.AllowPageRotation:
                    AllowPageRotation = value;
                    break;
                case SettingType.AllowPageArrangement:
                    AllowPageArrangement = value;
                    break;
                case SettingType.AllowPageCropping:
                    AllowPageCropping = value;
                    break;
                case SettingType.PageArrangementShowDeleteButton:
                    PageArrangementShowDeleteButton = value;
                    break;
                case SettingType.PageArrangementShowPageNumber:
                    PageArrangementShowPageNumber = value;
                    break;
                default:
                    Console.WriteLine("Invalid value");
                    break;
            }
        }

        public static void SetFilterValue(ScanFilter filter)
        {
            DefaultScanFilter = filter;
        }

        public static void SetColorValues(UIColor colorLight, UIColor colorDark, SettingType settingType)
        {
            switch (settingType)
            {
                case SettingType.ColorPrimary:
                    ColorPrimary = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), "dark", new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorSecondary:
                    ColorSecondary = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorOnSecondary:
                    ColorOnSecondary = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorScanButtonsLayoutBackground:
                    ColorScanButtonsLayoutBackground = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorScanButtonsForeground:
                    ColorScanButtonsForeground = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorScanPolygon:
                    ColorScanPolygon = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorBottomBarBackground:
                    ColorBottomBarBackground = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorBottomBarForeground:
                    ColorBottomBarForeground = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorTopBarBackground:
                    ColorTopBarBackground = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                case SettingType.ColorTopBarForeground:
                    ColorTopBarForeground = new NSDictionary(new NSString("light"), new NSString(colorLight.ToHexString()), new NSString("dark"), new NSString(colorDark.ToHexString()));
                    break;
                default:
                    Console.WriteLine("Invalid value");
                    break;
            }
        }
        public static void Reset()
        {
            //for the sake of simplicity, we reset all of the UserDefaults
            //if you want to use this in your own app, be aware that this deletes ALL of the userdefaults, not just the Docutain keys
            //it also causes the onboarding dialog to reappear when opening the camera
            //if you don't want that, you need to reset each single key manually
            foreach (var key in NSUserDefaults.StandardUserDefaults.ToDictionary().Keys)
            {
                NSUserDefaults.StandardUserDefaults.RemoveObject(key.ToString());
            }
        }
    }

}