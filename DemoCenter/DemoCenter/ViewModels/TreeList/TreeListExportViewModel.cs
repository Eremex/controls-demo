using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoCenter.DemoData;

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

        #region pdf export properties

        [ObservableProperty]
        private bool pdfExportColumnHeaders = true;

        [ObservableProperty]
        private bool pdfExportBandHeaders = true;

        [ObservableProperty]
        private bool fitToPageWidth = true;

        [ObservableProperty]
        private bool landscape = false;

        #endregion

        public IList<ApparelProduct> ApparelProducts { get; }

        public event Action<ExportType> RequestExport;

        [RelayCommand]
        private void Export(ExportType type)
        {
            RequestExport?.Invoke(type);
        }
    }
}
