using System.Globalization;

namespace FluentRule.Localization;
public static class MessageManager
{
    public static IMessageProvider Provider { get; set; } = new ResourceMessageProvider();

    public static string CurrentLanguage
    {
        get => CultureInfo.CurrentUICulture.Name;
        set => CultureInfo.CurrentUICulture = new CultureInfo(value);
    }
}