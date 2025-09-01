using Avalonia.Controls;
using DemoCenter.Helpers;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.DataControl;

namespace DemoCenter.Views
{
    public partial class DataGridExportView : UserControl
    {
        public DataGridExportView()
        {
            InitializeComponent();
        }

        private DataGridExportViewModel ViewModel { get; set; }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (ViewModel != null)
                ViewModel.RequestExport -= OnRequestExport;
            ViewModel = (DataGridExportViewModel)DataContext;
            if (ViewModel != null)
                ViewModel.RequestExport += OnRequestExport;
        }

        private void OnRequestExport(ExportType type)
        {
            if (type == ExportType.Xlsx)
            {
                var options = new XlsxExportOptions() { ShowColumnHeaders = ViewModel.ExportColumnHeaders, ShowBands = ViewModel.ExportBandHeaders };
                DemoExportHelper.Export(dataGrid, options, (stream, control, options) => control.ExportToXlsx(stream, options));
            } else if(type == ExportType.Pdf)
            {
                var options = new PageExportOptions() { ShowColumnHeaders = ViewModel.ExportColumnHeaders, ShowBands = ViewModel.ExportBandHeaders };
                DemoExportHelper.Export(dataGrid, options, (stream, control, options) => control.ExportToPdf(stream, options));
            }
        }
    }
}
