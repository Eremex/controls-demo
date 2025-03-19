using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoCenter.Views;

namespace DemoCenter.ProductsData
{
    public static class EditorsGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "Overview", title: "Overview",
                description: Resources.TheEremexControlsLibraryIncludesMultipleEd,
                viewModelGetter: () => new EditorsOverviewPageViewModel()),

                new PageInfo(name: "Text Editing", title: "Button & Text Editors", 
                description: Resources.TheEditorsLibraryIncludesTheTextEditorAndB,
                viewModelGetter: () => new TextEditingPageViewModel()),

                new PageInfo(name: "Spin Editor", title: "Spin Editor",
                description: Resources.TheSpinEditorIsANumericValueEditorWithBuil,
                viewModelGetter: () => new SpinEditorPageViewModel()),

                new PageInfo(name: "ComboBox Editor", title: "ComboBox Editor",
                description : Resources.TheComboBoxEditorFeaturesADropdownListOfIt,
                viewModelGetter: () => new ComboBoxEditorPageViewModel()),

                new PageInfo(name: "Segmented Editor", title: "Segmented Editor",
                description: Resources.YouCanUseTheSegmentedEditorToPresentASetOf,
                viewModelGetter: () => new SegmentedEditorPageViewModel()),

                new PageInfo(name: "Date Editor", title: "Date Editor",
                description: Resources.TheDateEditorFeaturesADropdownCalendarThat,
                viewModelGetter: () => new DateEditorPageViewModel()),

                new PageInfo(name: "Color Editor", title: "Color Editor",
                description: Resources.TheColorEditorAndPopupColorEditorControlsA,
                viewModelGetter: () => new ColorEditorPageViewModel()),

                new PageInfo(name: "Hyperlink Editor", title: "Hyperlink Editor",
                description: Resources.TheHyperlinkEditorDisplaysItsContentAsAHyp,
                viewModelGetter: () => new HyperlinkEditorPageViewModel(), new VersionInfo(1, 0)),

                new PageInfo(name: "Enum Source", title: "Enum Source", 
                description: Resources.YouCanCreateComboBoxAndSegmentedEditorsIte,
                viewModelGetter: () => new EnumSourcePageViewModel()),
                
                new PageInfo(name: "Memo Editor", title: "Memo Editor", 
                    description: Resources.UseMemoEditorToDisplayAndEditLargeTextInAP,
                    viewModelGetter: () => new MemoEditorPageViewModel(), new VersionInfo(1, 0)),
            };
        }
    }
}
