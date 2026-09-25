using DemoCenter.DemoData;

namespace DemoCenter.ViewModels;

public partial class DataGridTotalSummariesViewModel : PageViewModelBase
{
    public IList<ApparelSale> ApparelSales { get; }

    public DataGridTotalSummariesViewModel()
    {
        ApparelSales = DemoData.ApparelProducts.GenerateSales(1000);
    }
}
