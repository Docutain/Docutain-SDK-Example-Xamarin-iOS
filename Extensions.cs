using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace Docutain_SDK_Example_Xamarin_iOS
{

    internal static class Extensions
    {
        internal static string Localized(this string str)
        {
            return NSBundle.MainBundle.GetLocalizedString(str);
        }

        internal static string NameOfClass(this NSObject obj)
        {
            return obj.GetType().Name;
        }

        internal static string Identifier(this UITableViewCell cell)
        {
            return cell.NameOfClass();
        }

        internal static void RegisterCell<T>(this UITableView tableView) where T : UITableViewCell
        {
            tableView.RegisterClassForCellReuse(typeof(T), new NSString(typeof(T).Name));
        }

        internal static T DequeueCell<T>(this UITableView tableView) where T : UITableViewCell
        {
            var cell = tableView.DequeueReusableCell(new NSString(typeof(T).Name)) as T;
            return cell;
        }

        internal static UIColor ToUIColor(this string hexString)
        {
            hexString = hexString.TrimStart('#').Trim();
            if (hexString.Length != 6)
            {
                throw new ArgumentException("Invalid hex string.", nameof(hexString));
            }

            var r = Convert.ToInt32(hexString.Substring(0, 2), 16);
            var g = Convert.ToInt32(hexString.Substring(2, 2), 16);
            var b = Convert.ToInt32(hexString.Substring(4, 2), 16);

            return UIColor.FromRGB(r, g, b);
        }

        internal static string ToHexString(this UIColor color)
        {
            nfloat r, g, b, a;
            color.GetRGBA(out r, out g, out b, out a);

            var rInt = (int)(r * 255);
            var gInt = (int)(g * 255);
            var bInt = (int)(b * 255);

            return $"#{rInt:X2}{gInt:X2}{bInt:X2}";
        }

        internal static (UIColor light, UIColor dark) GetColors(this Dictionary<string, string> dictionary)
        {
            var lightColor = dictionary["light"].ToUIColor();
            var darkColor = dictionary["dark"].ToUIColor();
            return (lightColor, darkColor);
        }

        internal static Dictionary<string, string> ToStringDict (this NSDictionary dictionary) 
        {
            Dictionary<string, string> retVal = new Dictionary<string, string>((int)dictionary.Count);

            foreach (KeyValuePair<NSObject, NSObject> tuple in dictionary)
            {
                retVal.Add(tuple.Key.ToString(), tuple.Value.ToString());
            }


            return retVal;
        }

        public static string ToLowerFirstChar(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return char.ToLower(input[0]) + input.Substring(1);
        }

    }
}