using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public static class StandardControlsGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "Overview", title: "Overview",
                viewModelGetter: () => new StandardControlsOverviewPageViewModel()),

                new PageInfo(name: "Primitives", title: "Primitives",
                viewModelGetter: () => new PrimitivesPageViewModel()),

                new PageInfo(name: "ProgressBar", title: "ProgressBar",
                viewModelGetter: () => new ProgressBarPageViewModel()),

                new PageInfo(name: "Slider", title: "Slider",
                viewModelGetter: () => new SliderPageViewModel()),
            };
        }
    }
}