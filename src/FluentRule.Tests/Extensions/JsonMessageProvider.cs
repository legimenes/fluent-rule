using FluentRule.Localization;
using System.Globalization;
using System.Text.Json;

namespace FluentRule.Tests.Extensions;
public class JsonMessageProvider : IMessageProvider
{
    private readonly Dictionary<string, Dictionary<string, string>> _translations = [];

    public JsonMessageProvider(string jsonDirectoryPath)
    {
        LoadTranslations(jsonDirectoryPath);
    }

    private void LoadTranslations(string directory)
    {
        foreach (var file in Directory.GetFiles(directory, "messages.*.json"))
        {
            var cultureCode = Path.GetFileNameWithoutExtension(file).Split('.')[1];
            var content = File.ReadAllText(file);
            var messages = JsonSerializer.Deserialize<Dictionary<string, string>>(content)!;
            _translations[cultureCode] = messages;
        }
    }

    public string GetMessage(string key)
    {
        var culture = CultureInfo.CurrentUICulture.Name;

        if (_translations.TryGetValue(culture, out var dict)
            && dict.TryGetValue(key, out var message))
            return message;

        // fallback para inglês se não achar
        if (_translations.TryGetValue("en-US", out var enDict)
            && enDict.TryGetValue(key, out var defaultMessage))
            return defaultMessage;

        return key;
    }
}
