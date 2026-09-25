using Avalonia;
using Avalonia.Controls;

namespace DemoCenter.Views.ApplicationServices;

/// <summary>
/// The title and the one-paragraph summary shown at the top of every Application Services page.
/// </summary>
public partial class DemoPageHeader : UserControl
{
    public static readonly StyledProperty<string> TitleProperty =
        AvaloniaProperty.Register<DemoPageHeader, string>(nameof(Title));

    public static readonly StyledProperty<string> SummaryProperty =
        AvaloniaProperty.Register<DemoPageHeader, string>(nameof(Summary));

    public DemoPageHeader()
    {
        InitializeComponent();
    }

    public string Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public string Summary
    {
        get => GetValue(SummaryProperty);
        set => SetValue(SummaryProperty, value);
    }
}
