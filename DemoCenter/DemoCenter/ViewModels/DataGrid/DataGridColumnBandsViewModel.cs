using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using System.Collections;
using System.Collections.ObjectModel;

namespace DemoCenter.ViewModels;

public partial class DataGridColumnBandsViewModel : PageViewModelBase
{
    [ObservableProperty]
    IList sales;

    [ObservableProperty]
    IList<BandInfo> bands;

    public DataGridColumnBandsViewModel()
    {
        Sales = EmployeesData.GenerateComplexEmployeeSales();

        Bands = new ObservableCollection<BandInfo>();
        for (int i = 1; i < 4; i++)
        {
            var year = DateTime.Now.Year - i;
            var band = new BandInfo() { BandName = "" + year, Children = new List<BandInfo>() };
            for (int j = 0; j < 4; j++)
            {
                var quarterName = "Q" + (j + 1);
                band.Children.Add(new BandInfo() { BandName = year + "/" + quarterName, Header = quarterName });
            }
            Bands.Add(band);
        }
    }
}

public class BandInfo
{
    public string BandName { get; set; }

    public string Header { get; set; }

    public IList<BandInfo> Children { get; set; }
}
