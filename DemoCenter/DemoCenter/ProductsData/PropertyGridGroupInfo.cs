using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public static class PropertyGridGroupInfo
    {
        internal static List<PageInfo> Create()
        {
            return new List<PageInfo>()
            {
                new PageInfo(name: "Data Editors", title: "Data Editors", viewModelGetter: () => new PropertyGridDataEditorsViewModel(), descriptionGetter: () => Resources.PropertyGridAutomaticallyDetectsTheTypeOfB),
                new PageInfo(name: "Multiple Objects Editing", title: "Multiple Objects Editing", viewModelGetter: () => new PropertyGridMultipleObjectsViewModel(), descriptionGetter: () => Resources.PropertyGridMultipleObjectsDescription, introduced: new VersionInfo(1, 5)),
                new PageInfo(name: "Tab Items", title: "Tab Items", viewModelGetter: () => new PropertyGridTabItemsViewModel(), descriptionGetter: () => Resources.PropertyGridTabRowsAllowYouToGroupASetOfFi),
                new PageInfo(name: "Validation", title: "Validation", viewModelGetter: () => new PropertyGridValidationViewModel(), descriptionGetter: () => Resources.PropertyGridValidationDescription, introduced: new VersionInfo(1, 5)),
                new PageInfo(name: "PropertyGrid Layout", title: "PropertyGrid Layout", viewModelGetter: () => new PropertyGridLayoutViewModel(), descriptionGetter: () => Resources.PropertyGridLayoutDescription, introduced: new VersionInfo(1, 5))
            };
        }
    }
}