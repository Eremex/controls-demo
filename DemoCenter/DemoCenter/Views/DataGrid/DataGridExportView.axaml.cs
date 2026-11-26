using Avalonia.Controls;
using Avalonia.Interactivity;
using DemoCenter.Helpers;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.DataControl;
using Eremex.DocumentProcessing.Exports;

namespace DemoCenter.Views
{
    public partial class DataGridExportView : UserControl
    {
        public DataGridExportView()
        {
            InitializeComponent();
        }

        private void OnDataGridInitialized(object sender, EventArgs e)
        {
            dataGrid.CollapseGroupRow(-2);
            dataGrid.CollapseGroupRow(-1);
        }

        private DataGridExportViewModel ViewModel { get; set; }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (ViewModel != null)
            {
                ViewModel.RequestExport -= OnRequestExport;
                ViewModel.RequestExportImage -= OnRequestExportImage;
            }

            ViewModel = (DataGridExportViewModel)DataContext;
            if (ViewModel != null)
            {
                ViewModel.RequestExport += OnRequestExport;
                ViewModel.RequestExportImage += OnRequestExportImage;
            }
        }

        private void OnRequestExport(ExportType type)
        {
            if (type == ExportType.Xlsx)
            {
                var options = new XlsxExportOptions() { ShowColumnHeaders = ViewModel.XlsExportColumnHeaders, ShowBands = ViewModel.XlsExportBandHeaders, ShowTotalSummaries = ViewModel.XlsExportTotalSummaries, ShowGroupSummaries = ViewModel.XlsExportGroupSummaries };
                DemoExportHelper.Export(dataGrid, options, (stream, control, options) => control.ExportToXlsx(stream, options));
            } 
            else if(type == ExportType.Pdf)
            {
                var options = CreateExportOptions<PageExportOptions>();
                DemoExportHelper.Export(dataGrid, options, (stream, control, options) => control.ExportToPdf(stream, options));
            }
        }

        private void OnRequestExportImage(MxImageFormat format)
        {
            var options = CreateExportOptions<ImageExportOptions>();
            options.Format = format;
            DemoExportHelper.ExportImage(dataGrid, options, (control, options, dir, fileFormat) => control.ExportToImages(dir, fileFormat, options));
        }

        private T CreateExportOptions<T>() where T : PageExportOptions, new()
        {
            var options = new T
            {
                ShowColumnHeaders = ViewModel.PageExportColumnHeaders,
                ShowBands = ViewModel.PageExportBandHeaders,
                FitToPageWidth = ViewModel.FitToPageWidth,
                Landscape = ViewModel.Landscape,
                ShowTotalSummaries = ViewModel.PageExportTotalSummaries,
                ShowGroupSummaries = ViewModel.PageExportGroupSummaries
            };
            return options;
        }
    }
}
