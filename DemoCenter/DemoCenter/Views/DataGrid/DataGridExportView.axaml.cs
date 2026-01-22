using Avalonia.Controls;
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
                var options = new XlsxExportOptions() { ShowColumnHeaders = ViewModel.XlsExportColumnHeaders, ShowBands = ViewModel.XlsExportBandHeaders };
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

        private T CreateExportOptions<T>() where T : PageExportOptions
        {
            var options = Activator.CreateInstance<T>();
            options.ShowColumnHeaders = ViewModel.PageExportColumnHeaders;
            options.ShowBands = ViewModel.PageExportBandHeaders;
            options.FitToPageWidth = ViewModel.FitToPageWidth;
            options.Landscape = ViewModel.Landscape;
            return options;
        }
    }
}
