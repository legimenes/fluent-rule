namespace FluentRule.Localization;
public static class LocalizedMessages
{
    public static string CustomError => MessageManager.Provider.GetMessage(nameof(CustomError));
    public static string IsEnumName => MessageManager.Provider.GetMessage(nameof(IsEnumName));
    public static string MinimumLength => MessageManager.Provider.GetMessage(nameof(MinimumLength));
    public static string Must => MessageManager.Provider.GetMessage(nameof(Must));
    public static string NotNull => MessageManager.Provider.GetMessage(nameof(NotNull));
    public static string NotNullOrEmpty => MessageManager.Provider.GetMessage(nameof(NotNullOrEmpty));
    public static string NotNullOrWhiteSpace => MessageManager.Provider.GetMessage(nameof(NotNullOrWhiteSpace));
}