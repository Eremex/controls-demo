using Avalonia.Controls;

namespace DemoCenter.Views
{
    public partial class PropertyGridNestedPropertiesView : UserControl
    {
        public PropertyGridNestedPropertiesView()
        {
            InitializeComponent();

            propertyGrid.Initialized += PropertyGrid_Initialized;
        }

        private void PropertyGrid_Initialized(object sender, EventArgs e)
        {
            propertyGrid.ExpandAllRows();
        }
    }
}
