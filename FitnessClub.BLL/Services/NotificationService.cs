using Microsoft.Toolkit.Uwp.Notifications;

namespace FitnessClub.BLL.Services;

public static class NotificationService
{
    private const string ICON_ERROR_PATH = @"Images/error.png";
    private const string ICON_WARNING_PATH = @"Images/warning.png";
    private const string ICON_INFO_PATH = @"Images/info.png";

    private static ToastContentBuilder ToastContentWithArguments =>
        new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", 9813)
            .AddButton(new ToastButtonDismiss());

    public static void NotifyInfo(string title, string description) =>
        ToastContentWithArguments
            .AddAppLogoOverride(new Uri(Path.GetFullPath(ICON_INFO_PATH)))
            .AddText(title)
            .AddText(description)
            .Show();

    public static void NotifyError(string title, string description) =>
        ToastContentWithArguments
            .AddAppLogoOverride(new Uri(Path.GetFullPath(ICON_ERROR_PATH)))
            .AddText(title)
            .AddText(description)
            .Show();

    public static void NotifyWarning(string title, string description) =>
        ToastContentWithArguments
            .AddAppLogoOverride(new Uri(Path.GetFullPath(ICON_WARNING_PATH)))
            .AddText(title)
            .AddText(description)
            .Show();
}
