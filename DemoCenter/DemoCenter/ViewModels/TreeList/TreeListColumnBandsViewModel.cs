using DemoCenter.DemoData;
using System.Collections.ObjectModel;

namespace DemoCenter.ViewModels
{
    public partial class TreeListColumnBandsViewModel : PageViewModelBase
    {
        public TreeListColumnBandsViewModel()
        {
            Sales = SalesData.GenerateData();
        }

        public IList<SalesData> Sales { get; }
    }
}
