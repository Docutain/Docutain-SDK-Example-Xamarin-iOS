using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{

    public class BoolSetting : ISetting
    {
        public delegate void Handler(bool value, SettingType settingType);

        public SettingType SettingType { get; }
        public string Title { get; }
        public string Subtitle { get; }
        private bool _value;
        public bool Value
        {
            get => _value;
            set
            {
                _value = value;
                OnChangeHandler?.Invoke(_value, SettingType);
            }
        }
        public Handler OnChangeHandler { get; }

        public BoolSetting(SettingType settingType, bool initialValue, Handler onChangeHandler)
        {
            SettingType = settingType;
            Title = settingType.ToString().Localized();
            Subtitle = (Title.ToLowerFirstChar() + "_Description").Localized();
            Value = initialValue;
            OnChangeHandler = onChangeHandler;
        }
    }

}