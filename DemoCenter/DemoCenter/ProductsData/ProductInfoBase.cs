using DemoCenter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public class ProductInfoBase
    {
        public string Name { get; init; }
        public string Title { get; init; }
        public string Description { get; set; }
        public Func<PageViewModelBase> ViewModelGetter { get; init; }
        public virtual bool HasChildren => false;
        public ProductBadgeType? BadgeType { get; init; }
        public bool ShowInWeb { get; init; }
        public bool IsWebApp => App.IsWebApp;
    }

    public enum ProductBadgeType
    {
        New,
        Updated
    }
}
