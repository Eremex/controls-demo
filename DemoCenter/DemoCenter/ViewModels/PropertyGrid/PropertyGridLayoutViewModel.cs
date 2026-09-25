using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class PropertyGridLayoutViewModel : PageViewModelBase
    {
        [ObservableProperty]
        object selectedObject;

        public PropertyGridLayoutViewModel()
        {
            SelectedObject = CsvSources.Cars[0];
        }
    }
}
