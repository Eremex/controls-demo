using DemoCenter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData;

public class PageInfo : ProductInfoBase
{
    public override bool HasChildren => false;

    public PageInfo(string name, string title, Func<PageViewModelBase> viewModelGetter, Func<string> descriptionGetter = null, VersionInfo? introduced = null, VersionInfo? updated = null, bool showInWeb = true) : base(name, title, viewModelGetter, descriptionGetter, introduced, updated, showInWeb)
    {
    }
}
