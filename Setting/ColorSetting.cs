using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Essentials;

namespace Docutain_SDK_Example_Xamarin_iOS
{    

    public class ColorSetting : ISetting
    {
        public delegate void Handler(UIColor colorLight, UIColor colorDark, SettingType settingType);

        public SettingType SettingType { get; }
        public string Title { get; }
        public string Subtitle { get; }
        private UIColor _colorLight;
        public UIColor ColorLight
        {
            get => _colorLight;
            set
            {
                _colorLight = value;
                OnChangeHandler?.Invoke(_colorLight, _colorDark, SettingType);
            }
        }
        private UIColor _colorDark;
        public UIColor ColorDark
        {
            get => _colorDark;
            set
            {
                _colorDark = value;
                OnChangeHandler?.Invoke(_colorLight, _colorDark, SettingType);
            }
        }
        public Handler OnChangeHandler { get; }

        public ColorSetting(SettingType settingType, Dictionary<string, string> initialValue, Handler onChangeHandler)
        {
            SettingType = settingType;
            Title = settingType.ToString().Localized();
            Subtitle = (Title.ToLowerFirstChar() + "_Description").Localized();
            ColorLight = initialValue["light"].ToUIColor();
            ColorDark = initialValue["dark"].ToUIColor();
            this.OnChangeHandler = onChangeHandler;
        }
    }

}