using Avalonia.Controls;
using DemoCenter.Views.PropertyGrid.Utils;

namespace DemoCenter.Views
{
    public partial class PropertyGridTabItemsView : UserControl
    {
        public PropertyGridTabItemsView()
        {
            InitializeComponent();

            DataContext = new ContentControlPropertiesWrapper(contentControl);
        }
    }
}
