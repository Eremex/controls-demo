using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class DataGridGroupingPageViewModel : PageViewModelBase
    {
        [ObservableProperty]
        IList<EmployeeSale> sales;

        public DataGridGroupingPageViewModel()
        {
            Sales = EmployeesData.GenerateEmployeeSales();
        }
    }
}
