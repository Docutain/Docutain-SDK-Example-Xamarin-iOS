using Docutain.SDK.Xamarin.iOS;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{    
    public class PickerSetting : ISetting
    {
        public delegate void Handler(ScanFilter value, SettingType settingType);

        public SettingType SettingType { get; }
        public string Title { get; }
        public string Subtitle { get; }
        public List<ScanFilter> Items { get; }
        public ScanFilter Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChangeHandler?.Invoke(value, SettingType);
            }
        }

        private ScanFilter _value;
        private readonly Handler OnChangeHandler;

        public PickerSetting(SettingType settingType, ScanFilter initialValue, List<ScanFilter> items, Handler onChangeHandler)
        {
            Title = settingType.ToString().Localized();
            Subtitle = (Title.ToLowerFirstChar() + "_Description").Localized();
            Value = initialValue;
            SettingType = settingType;
            Items = items;
            OnChangeHandler = onChangeHandler;
        }
    }

}