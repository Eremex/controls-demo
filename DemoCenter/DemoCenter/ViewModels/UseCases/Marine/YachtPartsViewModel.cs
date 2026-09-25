using System.Collections.Generic;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;

using DemoCenter.DemoData;

namespace DemoCenter.ViewModels;

public partial class YachtPartsViewModel : PageViewModelBase
{
    [ObservableProperty] YachtInfo selectedYacht;
    [ObservableProperty] CarPartInfo selectedPart;
    [ObservableProperty] bool showOnlyBelowMinimumStock;
    [ObservableProperty] bool groupPartsByGroup = true;

    public YachtPartsViewModel()
    {
        Yachts = CsvSources.Yachts;
        SelectedYacht = Yachts.FirstOrDefault(x => x.Parts.Count > 0);
    }

    public IList<YachtInfo> Yachts { get; }

    public IReadOnlyList<CarPartInfo> Parts =>
        SelectedYacht == null
            ? new List<CarPartInfo>()
            : SelectedYacht.Parts.Where(x => !ShowOnlyBelowMinimumStock || x.IsBelowMinimumStock).ToList();

    public int PartsGroupCount => GroupPartsByGroup ? 1 : 0;

    partial void OnSelectedYachtChanged(YachtInfo value)
    {
        SelectedPart = Parts.FirstOrDefault();
        RaisePartsChanged();
    }

    partial void OnShowOnlyBelowMinimumStockChanged(bool value) => RaisePartsChanged();

    partial void OnGroupPartsByGroupChanged(bool value) => OnPropertyChanged(nameof(PartsGroupCount));

    void RaisePartsChanged() => OnPropertyChanged(nameof(Parts));
}
