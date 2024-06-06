using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{
    public interface ISettingCell
    {
        void Configure(ISetting option);
    }
}