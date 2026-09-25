using System.Windows.Input;

using Avalonia;
using Avalonia.Controls;

namespace DemoCenter.Views.ApplicationServices;

/// <summary>
/// One demo scenario: what it shows, why it matters, and the button that runs it.
/// </summary>
public partial class DemoScenario : UserControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<DemoScenario, string>(nameof(Title));

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<DemoScenario, string>(nameof(Description));

    public static readonly StyledProperty<string> ActionCaptionProperty =
        AvaloniaProperty.Register<DemoScenario, string>(nameof(ActionCaption), "Run");

    public static readonly StyledProperty<ICommand> CommandProperty =
        AvaloniaProperty.Register<DemoScenario, ICommand>(nameof(Command));

    public DemoScenario()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public string ActionCaption
    {
        get => GetValue(ActionCaptionProperty);
        set => SetValue(ActionCaptionProperty, value);
    }

    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}
