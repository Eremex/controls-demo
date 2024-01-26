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
                new PageInfo(name: "Data Editors", title: "Data Editors", description: "Property Grid automatically detects the type of bound fields, and uses appropriate Eremex data editors to display and edit cell values. You can explicitly assign editors to specific fields to override the default behavior and customize editor settings.", viewModelGetter: () => new PropertyGridDataEditorsViewModel()),
                new PageInfo(name: "Tab Items", title: "Tab Items", description: "Property Grid tab rows allow you to group a set of fields into a tabbed UI. Each tab item in a row displays its own set of bound fields.", viewModelGetter: () => new PropertyGridTabItemsViewModel())
            };
        }
    }
}