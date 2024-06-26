using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class DataGridRowAutoHeightViewModel : PageViewModelBase
    {
        [ObservableProperty]
        IList<CarInfo> cars;

        public DataGridRowAutoHeightViewModel()
        {
            Cars = CsvSources.Cars;
        }
    }
}
