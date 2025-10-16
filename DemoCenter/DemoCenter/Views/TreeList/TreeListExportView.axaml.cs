using Avalonia.Controls;
using DemoCenter.Helpers;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.DataControl;

namespace DemoCenter.Views
{
    public partial class TreeListExportView : UserControl
    {
        public TreeListExportView()
        {
            InitializeComponent();
        }

        private TreeListExportViewModel ViewModel { get; set; }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);

            if (ViewModel != null)
                ViewModel.RequestExport -= OnRequestExport;
            ViewModel = (TreeListExportViewModel)DataContext;
            if (ViewModel != null)
                ViewModel.RequestExport += OnRequestExport;
        }

        private void OnRequestExport(ExportType type)
        {
            if (type == ExportType.Xlsx)
            {
                var options = new XlsxExportOptions() { ShowColumnHeaders = ViewModel.XlsExportColumnHeaders, ShowBands = ViewModel.XlsExportBandHeaders };
                DemoExportHelper.Export(treeList, options, (stream, control, options) => control.ExportToXlsx(stream, options));
            }
            else if (type == ExportType.Pdf)
            {
                var options = new PageExportOptions() 
                { 
                    ShowColumnHeaders = ViewModel.PdfExportColumnHeaders, 
                    ShowBands = ViewModel.PdfExportBandHeaders,
                    FitToPageWidth = ViewModel.FitToPageWidth,
                    Landscape = ViewModel.Landscape,
                };
                DemoExportHelper.Export(treeList, options, (stream, control, options) => control.ExportToPdf(stream, options));
            }
        }
    }
}
