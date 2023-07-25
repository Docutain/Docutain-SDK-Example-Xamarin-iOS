using Foundation;

internal static class StringExtensions
{
    internal static string Localized(this string str)
    {
        return NSBundle.MainBundle.GetLocalizedString(str);
    }
}