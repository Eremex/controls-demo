using DemoCenter.DemoData;

namespace DemoCenter.ViewModels;

public partial class DataGridBestFitViewModel : PageViewModelBase
{   
    public IList<ApparelSale> ApparelSales { get; }

    public DataGridBestFitViewModel()
    {
        ApparelSales = DemoData.ApparelProducts.GenerateSales(5000);
    }
}