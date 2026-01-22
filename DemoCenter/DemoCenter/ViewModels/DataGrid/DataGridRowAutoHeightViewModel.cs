using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class DataGridRowAutoHeightViewModel : PageViewModelBase
    {
        public IList<CarInfo> Cars { get; } = CsvSources.Cars;
    }
}
