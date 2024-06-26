using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class DataGridValidationViewModel : PageViewModelBase
    {   
        [ObservableProperty]
        IList<EmployeeValidationInfo> employees;

        public DataGridValidationViewModel()
        {
            Employees = EmployeesData.GenerateValidationEmployeeInfo();
        }
    }
}
