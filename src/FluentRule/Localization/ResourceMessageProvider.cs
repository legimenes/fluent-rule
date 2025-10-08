using System.Globalization;
using System.Resources;

namespace FluentRule.Localization;
public class ResourceMessageProvider : IMessageProvider
{
    private readonly ResourceManager _resourceManager;

    public ResourceMessageProvider()
    {
        _resourceManager = new ResourceManager("FluentRule.Localization.Messages", typeof(ResourceMessageProvider).Assembly);
    }

    public string GetMessage(string key)
    {
        var culture = CultureInfo.CurrentUICulture;
        var message = _resourceManager.GetString(key, culture);

        if (string.IsNullOrEmpty(message))
            message = _resourceManager.GetString(key, new CultureInfo("en-US")) ?? key;

        return message;
    }
}