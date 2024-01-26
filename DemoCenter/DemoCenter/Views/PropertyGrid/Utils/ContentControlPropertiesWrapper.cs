using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Media.Immutable;

namespace DemoCenter.Views.PropertyGrid.Utils
{
    public class ContentControlPropertiesWrapper
    {
        ContentControl contentControl;

        double rotateAngle;
        double rotateCenterX;
        double rotateCenterY;
        double scaleX = 1;
        double scaleY = 1;
        bool mirrorX;
        bool mirrorY;

        public ContentControlPropertiesWrapper(ContentControl contentControl)
        {
            this.contentControl = contentControl;
        }

        public string Content
        {
            get => contentControl.Content as string;
            set => contentControl.Content = value;
        }

        public Color Foreground
        {
            get
            {
                if (contentControl.Foreground is SolidColorBrush solidColorBrush)
                    return solidColorBrush.Color;
                return Colors.Transparent;
            }
            set => contentControl.Foreground = new SolidColorBrush(value);
        }

        public string FontFamily
        {
            get => contentControl.FontFamily.Name;
            set => contentControl.FontFamily = new FontFamily(value);
        }

        public double FontSize
        {
            get => contentControl.FontSize;
            set => contentControl.FontSize = value;
        }

        public FontWeight FontWeight
        {
            get => contentControl.FontWeight;
            set => contentControl.FontWeight = value;
        }

        public FontStyle FontStyle
        {
            get => contentControl.FontStyle;
            set => contentControl.FontStyle = value;
        }

        public Color Background
        {
            get
            {
                if (contentControl.Background is ImmutableSolidColorBrush solidColorBrush)
                    return solidColorBrush.Color;
                return Colors.Transparent;
            }
            set => contentControl.Background = new SolidColorBrush(value).ToImmutable();
        }

        public Color BorderBrush
        {
            get
            {
                if (contentControl.BorderBrush is ImmutableSolidColorBrush solidColorBrush)
                    return solidColorBrush.Color;
                return Colors.Transparent;
            }
            set => contentControl.BorderBrush = new SolidColorBrush(value).ToImmutable();
        }

        public double BorderThickness
        {
            get => contentControl.BorderThickness.Left;
            set => contentControl.BorderThickness = new Thickness(value);
        }

        public double Opacity
        {
            get => contentControl.Opacity;
            set => contentControl.Opacity = value;
        }

        public double Left
        {
            get => contentControl.Margin.Left;
            set => contentControl.Margin = new Thickness(value, contentControl.Margin.Top, contentControl.Margin.Right, contentControl.Margin.Bottom);
        }

        public double Top
        {
            get => contentControl.Margin.Top;
            set => contentControl.Margin = new Thickness(contentControl.Margin.Left, value, contentControl.Margin.Right, contentControl.Margin.Bottom);
        }

        public double Right
        {
            get => contentControl.Margin.Right;
            set => contentControl.Margin = new Thickness(contentControl.Margin.Left, contentControl.Margin.Top, value, contentControl.Margin.Bottom);
        }

        public double Bottom
        {
            get => contentControl.Margin.Bottom;
            set => contentControl.Margin = new Thickness(contentControl.Margin.Left, contentControl.Margin.Top, contentControl.Margin.Right, value);
        }

        public HorizontalAlignment HorizontalAlignment
        {
            get => contentControl.HorizontalAlignment;
            set => contentControl.HorizontalAlignment = value;
        }

        public VerticalAlignment VerticalAlignment
        {
            get => contentControl.VerticalAlignment;
            set => contentControl.VerticalAlignment = value;
        }

        public HorizontalAlignment HorizontalContentAlignment
        {
            get => contentControl.HorizontalContentAlignment;
            set => contentControl.HorizontalContentAlignment = value;
        }

        public VerticalAlignment VerticalContentAlignment
        {
            get => contentControl.VerticalContentAlignment;
            set => contentControl.VerticalContentAlignment = value;
        }

        public double Width
        {
            get => contentControl.Width;
            set => contentControl.Width = value;
        }

        public double Height
        {
            get => contentControl.Height;
            set => contentControl.Height = value;
        }

        public double RotateAngle
        {
            get => rotateAngle;
            set
            {
                rotateAngle = value;
                UpdateRenderTransform();
            }
        }

        public double RotateCenterX
        {
            get => rotateCenterX;
            set
            {
                rotateCenterX = value;
                UpdateRenderTransform();
            }
        }

        public double RotateCenterY
        {
            get => rotateCenterY;
            set
            {
                rotateCenterY = value;
                UpdateRenderTransform();
            }
        }

        public double ScaleX
        {
            get => scaleX;
            set
            {
                scaleX = value;
                UpdateRenderTransform();
            }
        }

        public double ScaleY
        {
            get => scaleY;
            set
            {
                scaleY = value;
                UpdateRenderTransform();
            }
        }

        public bool MirrorX
        {
            get => mirrorX;
            set
            {
                mirrorX = value;
                UpdateRenderTransform();
            }
        }

        public bool MirrorY
        {
            get => mirrorY;
            set
            {
                mirrorY = value;
                UpdateRenderTransform();
            }
        }

        void UpdateRenderTransform()
        {
            var transformGroup = new TransformGroup();
            transformGroup.Children.Add(new RotateTransform(rotateAngle, rotateCenterX, rotateCenterY));
            transformGroup.Children.Add(new ScaleTransform(scaleX * (mirrorX ? -1 : 1), scaleY * (mirrorY ? -1 : 1)));
            contentControl.RenderTransform = transformGroup;
        }
    }
}
