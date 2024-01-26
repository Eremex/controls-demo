using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Avalonia;

using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;

namespace DemoCenter.ViewModels
{
    public partial class BarItemsPageViewModel : PageViewModelBase
    {
        public BarItemsPageViewModel()
        {
        }
        static List<Size> glyphSizes;
        public static List<Size> GlyphSizes
        {
            get
            {
                if (glyphSizes == null)
                    glyphSizes = new List<Size> { new Size(48, 48), new Size(32, 32), new Size(20, 20), new Size(16, 16) };
                return glyphSizes;
            }
        }
    }
}
