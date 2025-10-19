using FluentRule.Localization;

namespace FluentRule;
public static class FluentRuleOptions
{
    public static class GlobalOptions
    {
        public static void SetLanguage(string cultureCode)
        {
            MessageManager.CurrentLanguage = cultureCode;
        }

        public static void UseProvider(IMessageProvider provider)
        {
            MessageManager.Provider = provider;
        }
    }
}