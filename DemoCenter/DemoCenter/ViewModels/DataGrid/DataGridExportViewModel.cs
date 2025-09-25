using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public enum ExportType { Xlsx, Pdf }

    public partial class DataGridExportViewModel : PageViewModelBase
    {
        public DataGridExportViewModel()
        {
            ApparelProducts = DemoData.ApparelProducts.GenerateData(10000);
        }

        [ObservableProperty]
        private bool exportColumnHeaders = true;

        [ObservableProperty]
        private bool exportBandHeaders = true;

        public IList<ApparelProduct> ApparelProducts { get; }

        public event Action<ExportType> RequestExport;

        [RelayCommand]
        private void Export(ExportType type)
        {
            RequestExport?.Invoke(type);
        }
    }
}
