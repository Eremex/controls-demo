using Avalonia.Controls;
using Avalonia.Interactivity;

namespace DemoCenter.Views
{
    public partial class BarItemsPageView : UserControl
    {
        public BarItemsPageView()
        {
            InitializeComponent();
            PropertiesSelector.DataContext = ButtonItem;
        }

        private void OnRadioButtonCheckedChanged(object sender, RoutedEventArgs e)
        {
            var source = e.Source as RadioButton;

            if (source.IsChecked != true)
                return;
            if(source == ButtonItemSelector)
                PropertiesSelector.DataContext = ButtonItem;
            else if (source == ButtonCheckItemSelector)
                PropertiesSelector.DataContext = ButtonCheckItem;
            else if(source == ButtonDropDownItemSelector)
                PropertiesSelector.DataContext = ButtonDropDownItem;
            else if(source == TextItemSelector)
                PropertiesSelector.DataContext = TextItem;         
        }
    }
}
