using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using System.Collections.ObjectModel;

namespace DemoCenter.ViewModels
{
    public partial class DataGridMultipleSelectionPageViewModel : PageViewModelBase
    {   
        public DataGridMultipleSelectionPageViewModel()
        {
            Employees = EmployeesData.GenerateEmployeeInfo();
            SelectedEmployees = new(Employees.Take(5));
        }

        public ObservableCollection<EmployeeInfo> SelectedEmployees { get; }

        public IList<EmployeeInfo> Employees { get; }
    }
}
