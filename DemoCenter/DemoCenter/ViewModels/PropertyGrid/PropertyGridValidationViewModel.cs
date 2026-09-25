using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class PropertyGridValidationViewModel : PageViewModelBase
    {
        [ObservableProperty]
        YachtValidationInfo yacht;

        public PropertyGridValidationViewModel()
        {
            var index = Random.Shared.Next(CsvSources.Yachts.Count);
            Yacht = new YachtValidationInfo(CsvSources.Yachts.ElementAt(index));

            Yacht.Builder = string.Empty;
            Yacht.NumberOfCabins = 0;
            Yacht.RefitYear = Yacht.LaunchingYear - 3;
            Yacht.Price = 510000000;
            Yacht.Location = string.Empty;
        }
    }
}
