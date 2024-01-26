using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Data.Common;
using System.Windows.Input;

namespace DemoCenter.Views.Bars.Helpers;

public class ScaleDecorator : Decorator
{
    public static readonly StyledProperty<double> ScaleProperty = AvaloniaProperty.Register<ScaleDecorator, double>(nameof(Scale), defaultValue: 1);

    static ScaleDecorator()
    {
        ScaleProperty.Changed.AddClassHandler<ScaleDecorator>((x, e) => x.OnScaleChanged());
    }

    public ScaleDecorator()
    {
        DecreaseCommand = new RelayCommand(() => Scale -= 0.1, () => Scale > 0.2);
        IncreaseCommand = new RelayCommand(() => Scale += 0.1, () => Scale < 3);
        DefaultScaleCommand = new RelayCommand(() => Scale = 1, () => Scale != 1);
    }

    public double Scale
    {
        get => GetValue(ScaleProperty);
        set => SetValue(ScaleProperty, value);
    }

    public IRelayCommand DecreaseCommand { get; }

    public IRelayCommand IncreaseCommand { get; }

    public IRelayCommand DefaultScaleCommand { get; }

    private void OnScaleChanged()
    {
        DecreaseCommand.NotifyCanExecuteChanged();
        IncreaseCommand.NotifyCanExecuteChanged();
        DefaultScaleCommand.NotifyCanExecuteChanged();
        InvalidateArrange();
        Child.RenderTransform = new ScaleTransform(Scale, Scale);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var size = new Size(finalSize.Width / Scale, finalSize.Height / Scale);
        var position = new Point((finalSize.Width - size.Width) / 2, (finalSize.Height - size.Height) / 2);
        Child.Arrange(new Rect(position, size));
        return finalSize;
    }
}