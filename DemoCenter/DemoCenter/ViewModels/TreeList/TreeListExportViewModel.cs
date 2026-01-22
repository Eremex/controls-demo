using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoCenter.DemoData;
using Eremex.DocumentProcessing.Exports;

namespace DemoCenter.ViewModels
{
    public partial class TreeListExportViewModel : PageViewModelBase
    {
        public TreeListExportViewModel()
        {
            InfrastructureItems = InfrastructureData.GenerateData();
        }

        public List<InfrastructureItem> InfrastructureItems { get; }

        #region xlsx export properties

        [ObservableProperty]
        private bool xlsExportColumnHeaders = true;

        [ObservableProperty]
        private bool xlsExportBandHeaders = true;

        #endregion

        #region page export properties

        [ObservableProperty]
        private bool pageExportColumnHeaders = true;

        [ObservableProperty]
        private bool pageExportBandHeaders = true;

        [ObservableProperty]
        private bool fitToPageWidth = true;

        [ObservableProperty]
        private bool landscape = false;

        #endregion

        public IList<ApparelProduct> ApparelProducts { get; }

        public event Action<ExportType> RequestExport;
        public event Action<MxImageFormat> RequestExportImage;

        [RelayCommand]
        private void Export(ExportType type)
        {
            RequestExport?.Invoke(type);
        }

        [RelayCommand]
        private void ExportImage(MxImageFormat format)
        {
            RequestExportImage?.Invoke(format);
        }
    }
}
