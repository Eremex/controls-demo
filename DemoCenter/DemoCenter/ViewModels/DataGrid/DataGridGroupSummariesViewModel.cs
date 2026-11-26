using DemoCenter.DemoData;

namespace DemoCenter.ViewModels;

public partial class DataGridGroupSummariesViewModel : PageViewModelBase
{
    public IList<OrderData> Orders { get; }

    public DataGridGroupSummariesViewModel()
    {
        Orders = OrderData.GenerateData(500);
    }
}
