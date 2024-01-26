using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Editors;

namespace DemoCenter.ViewModels
{
    public partial class ColorEditorPageViewModel : PageViewModelBase
    {
        [ObservableProperty] bool showStandardColors = true;
        [ObservableProperty] bool showCustomColors = true;
        [ObservableProperty] ColorsShowMode colorsShowMode = ColorsShowMode.StandardColors | ColorsShowMode.CustomColors;

        [ObservableProperty] ObservableCollection<Color> customColors1 = new ObservableCollection<Color>() 
        { 
            Color.FromRgb(0x7d, 0xd7, 0xab), Color.FromRgb(0xc5, 0x94, 0x88), Color.FromRgb(0x47, 0xfe, 0xff), Color.FromRgb(0xe9, 0xbf, 0x3f), 
        };
        [ObservableProperty] ObservableCollection<Color> customColors2 = new ObservableCollection<Color>()
        {
            Color.FromRgb(0x7d, 0x82, 0x82), Color.FromRgb(0xa1, 0x17, 0x2a), Color.FromRgb(0x58, 0xd9, 0xdb), Colors.Yellow,
        };

        public ColorEditorPageViewModel()
        {
        }

        partial void OnShowStandardColorsChanged(bool value)
        {
            if(value)
                ColorsShowMode |= ColorsShowMode.StandardColors;
            else
                ColorsShowMode &= ~ColorsShowMode.StandardColors;
        }
        partial void OnShowCustomColorsChanged(bool value)
        {
            if (value)
                ColorsShowMode |= ColorsShowMode.CustomColors;
            else
                ColorsShowMode &= ~ColorsShowMode.CustomColors;
        }

    }
}
