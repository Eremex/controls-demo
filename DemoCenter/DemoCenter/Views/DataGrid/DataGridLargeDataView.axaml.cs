using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using DemoCenter.DemoData;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.DataGrid;
using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.Views
{
    public partial class DataGridLargeDataView : UserControl
    {
        Random random = new Random();

        Dictionary<Tuple<int, string>, object> valuesCache = new Dictionary<Tuple<int, string>, object>();

        public DataGridLargeDataView()
        {
            InitializeComponent();
        }

        private void DataGridControl_PropertyChanged(object sender, AvaloniaPropertyChangedEventArgs e)
        {
            if(e.Property == DataGridControl.ItemsSourceProperty)
                valuesCache.Clear();
        }

        void DataGridControl_CustomUnboundColumnData(object sender, DataGridUnboundColumnDataEventArgs e)
        {   
            var key = Tuple.Create(((LargeDataItem)e.Item).Id, e.Column.FieldName);
            if (e.IsGettingData)
            {
                if (!valuesCache.TryGetValue(key, out var value))
                {
                    value = GenerateColumnValue(e.Column);
                    valuesCache[key] = value;
                }
                e.Value = value;
            }
            else
            {
                valuesCache[key] = e.Value;
            }
        }

        object GenerateColumnValue(GridColumn column)
        {

            if (column.UnboundDataType == typeof(int))
            {
                return random.Next(1000);
            }
            else if (column.UnboundDataType == typeof(DateTime))
            {
                return DateTime.Today.AddDays(-random.Next(1000));
            }
            else if (column.UnboundDataType == typeof(bool))
            {
                return random.Next(2) % 2 == 0;
            }
            else
            {
                return EmployeesData.EmployeeNames[random.Next(EmployeesData.EmployeeNames.Count)];
            }
        }
    }

    public class DataGridLargeDataViewColumnTemplate : ITemplate<object, GridColumn>
    {
        public GridColumn Build(object param)
        {
            var largeDataColumn = (LargeDataColumn)param;
            var gridColumn = new GridColumn() 
            { 
                FieldName = largeDataColumn.FieldName,
                Header = largeDataColumn.Header
            };
            if (!largeDataColumn.FieldName.Contains("Id"))
            {
                gridColumn.UnboundDataType = largeDataColumn.DataType;

                if (largeDataColumn.FieldName.Contains("ComboBox"))
                    gridColumn.EditorProperties = new ComboBoxEditorProperties() { ItemsSource = EmployeesData.EmployeeNames };
                else if (largeDataColumn.FieldName.Contains("Numeric"))
                    gridColumn.EditorProperties = new SpinEditorProperties() { MaskType = MaskType.Numeric, Mask = "c" };
            }
            return gridColumn;
        }
    }
}
