using Avalonia.Controls;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls.DataGrid;
using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.Views
{
    public partial class DataGridValidationView : UserControl
    {
        public DataGridValidationView()
        {
            InitializeComponent();
        }

        private void OnValidateCellValue(object sender, DataGridValidateCellValueEventArgs e)
        {
            if(e.Column.FieldName == nameof(EmployeeValidationInfo.HireDate) && chkValidateHireDate.IsChecked == true)
            {
                var birthDate = (DateTime)dataGrid.GetCellValue(e.RowIndex, birthDateColumn);
                if ((DateTime)e.Value < birthDate)
                    e.ErrorContent = "Hire Date cannot be less than Birth Date";
            }
            else if(e.Column.FieldName == nameof(EmployeeValidationInfo.Experience) && chkValidateExperience.IsChecked == true) 
            {
                if ((int)e.Value < 1)
                    e.ErrorContent = "Experience cannot be less than one year";
            }
        }
    }
}
