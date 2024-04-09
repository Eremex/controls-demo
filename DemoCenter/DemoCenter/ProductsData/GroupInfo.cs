using DemoCenter.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ProductsData
{
    public class GroupInfo : ProductInfoBase
    {
        public List<PageInfo> Pages { get; set; }

        public override bool HasChildren => Pages.Count > 0;

        public GroupInfo(string name, string title, string description, Func<PageViewModelBase> viewModel, List<PageInfo> pages, ProductBageType? bageType = null, bool showInWeb = true)
        {
            Name = name.ToUpper();
            Title = title;
            Description = description;
            ViewModelGetter = viewModel;
            Pages = GetAppProducts(pages);
            BageType = bageType;
            ShowInWeb = showInWeb;
        }
        public static List<T> GetAppProducts<T>(List<T> source, bool checkHasChildren = false) where T : ProductInfoBase
            => source.Where(x => (!App.IsWebApp || x.ShowInWeb) && (!checkHasChildren || x.HasChildren)).ToList();
    }
}
