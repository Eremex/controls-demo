using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class DataGridFixedColumnsViewModel : PageViewModelBase
    {

        [ObservableProperty]
        IList<EmployeeInfo> employees;

        [ObservableProperty]
        bool autoHideScrollbars = true;

        public DataGridFixedColumnsViewModel()
        {
            Employees = EmployeesData.GenerateEmployeeInfo();
        }
    }
}
