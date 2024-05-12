using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using CommunityToolkit.Mvvm.ComponentModel;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.ViewModels
{
    public partial class ComboBoxEditorPageViewModel : PageViewModelBase
    {
        [ObservableProperty] string yachtValue = CsvSources.YachtNames[3];
        [ObservableProperty] FilterCondition editableViewFilterCondition = FilterCondition.StartsWith;

        [ObservableProperty] List<ElementInfo> elements;
        [ObservableProperty] ElementInfo selectedElement;

        [ObservableProperty] IList<MechInfo> mechs;
        [ObservableProperty] ObservableCollection<MechInfo> selectedMechs;

        [ObservableProperty] string[] separators = new[] { ";", ".", "...", "|" };
        [ObservableProperty] string selectedSeparator;

        public ComboBoxEditorPageViewModel()
        {
            Elements = ElementsSources.Elements;
            SelectedElement = Elements[9];

            Mechs = CsvSources.Mechs;
            SelectedMechs = new ObservableCollection<MechInfo>() { Mechs[1], Mechs[5], Mechs[6], Mechs[9] };
            SelectedSeparator = Separators[0];
        }
    }
}
