using DemoCenter.ViewModels.ApplicationServices;

namespace DemoCenter.ProductsData;

/// <summary>
/// Pages of the Application Services category.
/// </summary>
/// <remarks>
/// Every page opens real windows and dialogs, so none of them is shown in the web demo.
/// </remarks>
public class ApplicationServicesGroupInfo
{
    internal static List<PageInfo> Create()
    {
        var introduced = new VersionInfo(1, 5);

        return new List<PageInfo>()
        {
            new PageInfo(
                name: "Registration",
                title: "Service Registration",
                viewModelGetter: () => new ServiceRegistrationPageViewModel(),
                descriptionGetter: () => Resources.ApplicationServicesRegistrationDescription,
                introduced: introduced,
                showInWeb: false),
            new PageInfo(
                name: "Message Boxes",
                title: "Message Boxes",
                viewModelGetter: () => new MessageBoxServicePageViewModel(),
                descriptionGetter: () => Resources.ApplicationServicesMessageBoxesDescription,
                introduced: introduced,
                showInWeb: false),

            new PageInfo(
                name: "Dialogs",
                title: "Dialogs",
                viewModelGetter: () => new DialogServicePageViewModel(),
                descriptionGetter: () => Resources.ApplicationServicesDialogsDescription,
                introduced: introduced,
                showInWeb: false),

            new PageInfo(
                name: "Choice Dialogs",
                title: "Choice Dialogs",
                viewModelGetter: () => new ChoiceDialogServicePageViewModel(),
                descriptionGetter: () => Resources.ApplicationServicesChoiceDialogsDescription,
                introduced: introduced,
                showInWeb: false),

            new PageInfo(
                name: "Windows",
                title: "Windows",
                viewModelGetter: () => new WindowServicePageViewModel(),
                descriptionGetter: () => Resources.ApplicationServicesWindowsDescription,
                introduced: introduced,
                showInWeb: false),

            new PageInfo(
                name: "File Dialogs",
                title: "File Dialogs",
                viewModelGetter: () => new FileDialogServicePageViewModel(),
                descriptionGetter: () => Resources.ApplicationServicesFileDialogsDescription,
                introduced: introduced,
                showInWeb: false),

        };
    }
}
