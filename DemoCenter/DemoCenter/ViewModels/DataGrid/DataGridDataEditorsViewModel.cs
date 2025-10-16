using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ViewModels
{
    public partial class DataGridDataEditorsViewModel : PageViewModelBase
    {   

        [ObservableProperty]
        IList<EmployeeInfo> employees;

        public DataGridDataEditorsViewModel()
        {
            Employees = EmployeesData.GenerateEmployeeInfo();
        }
    }
}
