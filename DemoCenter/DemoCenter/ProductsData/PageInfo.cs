using DemoCenter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public class PageInfo : ProductInfoBase
    {
        public PageInfo(string name, string title, string description, Func<PageViewModelBase> viewModelGetter)
        {
            Name = name;
            Title = title;
            Description = description;
            ViewModelGetter = viewModelGetter;
        }
    }
}
