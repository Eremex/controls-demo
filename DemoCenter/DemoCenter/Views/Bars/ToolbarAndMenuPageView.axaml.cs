using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace DemoCenter.Views
{
    public partial class ToolbarAndMenuPageView : UserControl
    {
        public static readonly AttachedProperty<bool> IsToolbarSelectedProperty =
            AvaloniaProperty.RegisterAttached<ToolbarAndMenuPageView, AvaloniaObject, bool>("IsToolbarSelected");

        public static bool GetIsToolbarSelected(AvaloniaObject target)
        {
            return target.GetValue(IsToolbarSelectedProperty);
        }

        public static void SetIsToolbarSelected(AvaloniaObject target, bool isToolbarSelected)
        {
            target.SetValue(IsToolbarSelectedProperty, isToolbarSelected);
        }

        IReadOnlyList<string> fonts;

        public IReadOnlyList<string> Fonts =>
            fonts ?? (fonts = FontManager.Current.SystemFonts.Select(x => x.Name).OrderBy(x => x).ToList());

        public IReadOnlyList<double> FontSizes { get; } = new List<double>()
            { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };



        public ToolbarAndMenuPageView()
        {
            InitializeComponent();

            PropertiesSelector.DataContext = MainMenu;
        }

        private void OnRadioButtonCheckedChanged(object sender, RoutedEventArgs e)
        {
            var source = e.Source as RadioButton;

            if(source.IsChecked != true)
                return;

            if(PropertiesSelector.DataContext is AvaloniaObject oldToolbar)
                SetIsToolbarSelected(oldToolbar, false);

            if(source == MainMenuSelector)
                PropertiesSelector.DataContext = MainMenu;
            else if(source == FileToolbarSelector)
                PropertiesSelector.DataContext = FileToolbar;
            else if(source == EditToolbarSelector)
                PropertiesSelector.DataContext = EditToolbar;
            else if(source == FontToolbarSelector)
                PropertiesSelector.DataContext = FontToolbar;
            else if(source == StatusBarSelector)
                PropertiesSelector.DataContext = StatusBar;

            SetIsToolbarSelected((AvaloniaObject)PropertiesSelector.DataContext, true);
        }
    }
}
