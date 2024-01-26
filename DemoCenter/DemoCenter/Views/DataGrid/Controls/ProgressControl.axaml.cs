using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace DemoCenter.Views.DataGrid.Controls
{
    public partial class ProgressControl : UserControl
    {
        public static readonly StyledProperty<IBrush> ColorProperty = AvaloniaProperty.Register<ProgressControl, IBrush>(nameof(Color));

        public static readonly StyledProperty<double> ValueProperty = AvaloniaProperty.Register<ProgressControl, double>(nameof(Value));

        public static readonly StyledProperty<double> MaximumProperty = AvaloniaProperty.Register<ProgressControl, double>(nameof(Maximum));

        public ProgressControl()
        {
            InitializeComponent();
        }

        public IBrush Color
        {
            get => GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public double Value
        {
            get => GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double Maximum
        {
            get => GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double ratio = Value / Maximum;
            border!.Width = Math.Ceiling(availableSize.Width * ratio);
            return base.MeasureOverride(availableSize);
        }
    }
}
