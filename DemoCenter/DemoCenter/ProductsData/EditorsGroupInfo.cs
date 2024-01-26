using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public static class EditorsGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "Overview", title: "Overview",
                description: "The Eremex Controls library includes multiple editors that provide you with advanced data editing capabilities. The editors allow you to display and edit data of different data types (numeric, Boolean, date-time, enumerations, etc.). They support the data validation mechanism, styling, and embedding in container controls (Data Grid, Tree List, Property Grid, and Toolbars/Menus).",
                viewModelGetter: () => new EditorsOverviewPageViewModel()),

                new PageInfo(name: "Text Editing", title: "Button & Text Editors", 
                description: "The Editors library includes the Text Editor and Button Editor controls that provide base text editing capabilities: data validation, watermarks (hints displayed when the editor is empty), and multiple options to control text selection and data edit operations. The Button Editor supports an unlimited number of built-in regular and check buttons, which can display text or images at the left or right edge of the edit box.",
                viewModelGetter: () => new TextEditingPageViewModel()),

                new PageInfo(name: "Spin Editor", title: "Spin Editor",
                description: "The Spin Editor is a numeric value editor with built-in spin buttons used to increase and decrease a number by a specific value (increment). A user can increment and decrement the number by clicking these buttons, or by pressing the Up and Down Arrows on the keyboard.",
                viewModelGetter: () => new SpinEditorPageViewModel()),

                new PageInfo(name: "ComboBox Editor", title: "ComboBox Editor",
                description : "The ComboBox Editor features a dropdown list of items from which a user can select one or more items. The editor can display a list of strings, a list of business objects, or enumeration values. In multi-select mode, item check boxes allow the user to select multiple items at a time.",
                viewModelGetter: () => new ComboBoxEditorPageViewModel()),

                new PageInfo(name: "Segmented Editor", title: "Segmented Editor",
                description: "You can use the Segmented Editor to present a set of options as horizontally arranged segments. A user can click one of the segments to select a corresponding option, or CTRL-click on a selected segment to clear the selection. The editor allows you to populate segments from a list of strings, a list of business objects, or an enumeration type.",
                viewModelGetter: () => new SegmentedEditorPageViewModel()),

                new PageInfo(name: "Date Editor", title: "Date Editor",
                description: "The Date Editor features a dropdown calendar that allows users to select a date. The calendar's navigation bar enables the user to browse through months and years. You can format the selected date in a specific manner by setting one of numerous display formats.",
                viewModelGetter: () => new DateEditorPageViewModel()),

                new PageInfo(name: "Color Editor", title: "Color Editor",
                description: "The Color Editor and Popup Color Editor controls allow a user to select a color. The controls support three color palettes: default (customizable in code), standard (fixed colors), and custom (customizable by users). The built-in Color Picker helps the user add custom colors from a color space, or specify color values in the RGB and HSB formats.",
                viewModelGetter: () => new ColorEditorPageViewModel()),

                new PageInfo(name: "Hyperlink Editor", title: "Hyperlink Editor",
                description: "The Hyperlink Editor displays its contents as a hyperlink. Bind a command to the editor to process hyperlink clicks.",
                viewModelGetter: () => new HyperlinkEditorPageViewModel()),

                new PageInfo(name: "Enum Source", title: "Enum Source", 
                description: "You can create ComboBox and Segmented Editors' items from enumeration type values. Dedicated Data Annotation attributes applied to the enumeration values allow you to populate the controls' items with images, display text and tooltips.",
                viewModelGetter: () => new EnumSourcePageViewModel()),
            };
        }
    }
}