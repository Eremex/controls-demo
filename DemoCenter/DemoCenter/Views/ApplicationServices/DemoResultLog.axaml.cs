using System.Collections;
using System.Windows.Input;

using Avalonia;
using Avalonia.Controls;

namespace DemoCenter.Views.ApplicationServices;

/// <summary>
/// Shows what each demo button returned, newest entry first.
/// </summary>
public partial class DemoResultLog : UserControl
{
    public static readonly StyledProperty<IEnumerable> ItemsSourceProperty =
        AvaloniaProperty.Register<DemoResultLog, IEnumerable>(nameof(ItemsSource));

    public static readonly StyledProperty<ICommand> ClearCommandProperty =
        AvaloniaProperty.Register<DemoResultLog, ICommand>(nameof(ClearCommand));

    public DemoResultLog()
    {
        InitializeComponent();
    }

    public IEnumerable ItemsSource
    {
        get => GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public ICommand ClearCommand
    {
        get => GetValue(ClearCommandProperty);
        set => SetValue(ClearCommandProperty, value);
    }
}
