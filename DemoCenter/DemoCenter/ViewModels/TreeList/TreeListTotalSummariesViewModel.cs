using DemoCenter.DemoData;

namespace DemoCenter.ViewModels;

public partial class TreeListTotalSummariesViewModel : PageViewModelBase
{
    public List<InfrastructureItem> InfrastructureItems { get; }

    public TreeListTotalSummariesViewModel()
    {
        InfrastructureItems = InfrastructureData.GenerateData();
    }
}
