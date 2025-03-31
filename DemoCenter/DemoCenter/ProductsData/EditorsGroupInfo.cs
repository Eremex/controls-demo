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
                viewModelGetter: () => new EditorsOverviewPageViewModel(), descriptionGetter: () => Resources.TheEremexControlsLibraryIncludesMultipleEd),

                new PageInfo(name: "Text Editing", title: "Button & Text Editors",
                viewModelGetter: () => new TextEditingPageViewModel(), descriptionGetter: () => Resources.TheEditorsLibraryIncludesTheTextEditorAndB),

                new PageInfo(name: "Spin Editor", title: "Spin Editor",
                viewModelGetter: () => new SpinEditorPageViewModel(), descriptionGetter: () => Resources.TheSpinEditorIsANumericValueEditorWithBuil),

                new PageInfo(name: "ComboBox Editor", title: "ComboBox Editor",
                viewModelGetter: () => new ComboBoxEditorPageViewModel(), descriptionGetter: () => Resources.TheComboBoxEditorFeaturesADropdownListOfIt),

                new PageInfo(name: "Segmented Editor", title: "Segmented Editor",
                viewModelGetter: () => new SegmentedEditorPageViewModel(), descriptionGetter: () => Resources.YouCanUseTheSegmentedEditorToPresentASetOf),

                new PageInfo(name: "Date Editor", title: "Date Editor",
                viewModelGetter: () => new DateEditorPageViewModel(), descriptionGetter: () => Resources.TheDateEditorFeaturesADropdownCalendarThat),

                new PageInfo(name: "Color Editor", title: "Color Editor",
                viewModelGetter: () => new ColorEditorPageViewModel(), descriptionGetter: () => Resources.TheColorEditorAndPopupColorEditorControlsA),

                new PageInfo(name: "Hyperlink Editor", title: "Hyperlink Editor",
                viewModelGetter: () => new HyperlinkEditorPageViewModel(), descriptionGetter: () => Resources.TheHyperlinkEditorDisplaysItsContentAsAHyp, introduced: new VersionInfo(1, 0)),

                new PageInfo(name: "Enum Source", title: "Enum Source",
                viewModelGetter: () => new EnumSourcePageViewModel(), descriptionGetter: () => Resources.YouCanCreateComboBoxAndSegmentedEditorsIte),
                
                new PageInfo(name: "Memo Editor", title: "Memo Editor",
                    viewModelGetter: () => new MemoEditorPageViewModel(), descriptionGetter: () => Resources.UseMemoEditorToDisplayAndEditLargeTextInAP, introduced: new VersionInfo(1, 0)),
            };
        }
    }
}
