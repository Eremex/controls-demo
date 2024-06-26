using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class DataGridFilteringViewModel : PageViewModelBase
    {   
        [ObservableProperty]
        IList<EmployeeInfo> employees;

        public DataGridFilteringViewModel()
        {
            Employees = EmployeesData.GenerateEmployeeInfo();
        }
    }
}
