using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class PropertyGridMultipleObjectsViewModel : PageViewModelBase
    {
        [ObservableProperty]
        ObservableCollection<EmployeeInfo> employees;

        [ObservableProperty]
        ObservableCollection<EmployeeInfo> selectedEmployees;

        public PropertyGridMultipleObjectsViewModel()
        {   
            Employees = new ObservableCollection<EmployeeInfo>(EmployeesData.GenerateEmployeeInfo());

            SetSameProperties(Employees[1], Employees[0]);
            SetSameProperties(Employees[2], Employees[0]);

            void SetSameProperties(EmployeeInfo targetItem, EmployeeInfo sourceItem)
            {
                targetItem.Position = sourceItem.Position;
                targetItem.City = sourceItem.City;
                targetItem.EmploymentType = sourceItem.EmploymentType;
            }

            SelectedEmployees = new ObservableCollection<EmployeeInfo>(Employees.Take(3));
        }
    }
}
