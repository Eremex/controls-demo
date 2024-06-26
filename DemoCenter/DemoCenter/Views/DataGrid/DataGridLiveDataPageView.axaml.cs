using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;
using DemoCenter.ViewModels;
using System.Globalization;

namespace DemoCenter.Views
{
    public partial class DataGridLiveDataPageView : UserControl
    {
        DataGridLiveDataPageViewModel ViewModel => (DataGridLiveDataPageViewModel)DataContext;

        public DataGridLiveDataPageView()
        {
            InitializeComponent();
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            ViewModel.RunUpdate();
        }

        protected override void OnUnloaded(RoutedEventArgs e)
        {
            ViewModel.StopUpdate();
            base.OnUnloaded(e);
        }
    }

    public class LiveDataColumnHeaderControl : TemplatedControl
    {
        public static readonly StyledProperty<double?> ValueProperty = 
            AvaloniaProperty.Register<LiveDataColumnHeaderControl, double?>(nameof(Value));

        public static readonly StyledProperty<double> MaxValueProperty = 
            AvaloniaProperty.Register<LiveDataColumnHeaderControl, double>(nameof(MaxValue), 1);

        public static readonly StyledProperty<object> HeaderProperty = 
            AvaloniaProperty.Register<LiveDataColumnHeaderControl, object>(nameof(Header));

        public static readonly DirectProperty<LiveDataColumnHeaderControl, string> DisplayTextProperty = 
            AvaloniaProperty.RegisterDirect<LiveDataColumnHeaderControl, string>(
                nameof(DisplayText),
                o => o.DisplayText);

        static LiveDataColumnHeaderControl()
        {
            ValueProperty.Changed.AddClassHandler<LiveDataColumnHeaderControl>((x, e) => x.UpdateDisplayText());
            MaxValueProperty.Changed.AddClassHandler<LiveDataColumnHeaderControl>((x, e) => x.UpdateDisplayText());
        }

        string displayText;

        public double? Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double MaxValue
        {
            get => GetValue(MaxValueProperty);
            set => SetValue(MaxValueProperty, value);
        }

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public string DisplayText
        {
            get { return displayText; }
            private set { SetAndRaise(DisplayTextProperty, ref displayText, value); }
        }

        void UpdateDisplayText()
        {
            if (Value == null)
                DisplayText = string.Empty;
            if (Value == 0)
                DisplayText = string.Format("{0:p0}", 0);
            else
                DisplayText = string.Format("{0:p1}", Value / MaxValue);
        }
    }

    public class LiveDataBorder : Border
    {
        public static readonly StyledProperty<object> ValueProperty =
            AvaloniaProperty.Register<LiveDataBorder, object>(nameof(Value));

        public static readonly StyledProperty<double> ThresholdProperty =
            AvaloniaProperty.Register<LiveDataBorder, double>(nameof(Threshold));

        public static readonly StyledProperty<SolidColorBrush> BasicBrushProperty =
            AvaloniaProperty.Register<LiveDataBorder, SolidColorBrush>(nameof(BasicBrush));


        static LiveDataBorder()
        {
            ValueProperty.Changed.AddClassHandler<LiveDataBorder>((x, e) => x.UpdateBackground());
            ThresholdProperty.Changed.AddClassHandler<LiveDataBorder>((x, e) => x.UpdateBackground());
            BasicBrushProperty.Changed.AddClassHandler<LiveDataBorder>((x, e) => x.UpdateBackground());
        }

        public object Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Threshold
        {
            get => GetValue(ThresholdProperty);
            set => SetValue(ThresholdProperty, value);
        }

        public SolidColorBrush BasicBrush
        {
            get => GetValue(BasicBrushProperty);
            set => SetValue(BasicBrushProperty, value);
        }

        void UpdateBackground()
        {
            Background = GetBackground();
        }

        IBrush GetBackground()
        {
            if (BasicBrush == null || Value is not double value)
                return Brushes.Transparent;

            var opacity = 0.4d;
            if (value > Threshold)
                opacity = 0.8;
            else if (value > 0)
                opacity = 0.6;
            return new SolidColorBrush(BasicBrush.Color) { Opacity = opacity };
        }
    }

    public class LiveDataValueToStringConverter : IValueConverter
    {
        public static IValueConverter Instance { get; } = new LiveDataValueToStringConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double result)
            {   
                if (result == 0)
                    return string.Format("0{0}", parameter);
                return string.Format("{0:f1}{1}", result, parameter);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return BindingOperations.DoNothing;
        }
    }
}
