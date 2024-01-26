using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class EditorsOverviewPageViewModel : PageViewModelBase
    {
        [ObservableProperty] DateTime selectedDate = DateTime.Today.AddDays(3);
        [ObservableProperty] GraphicView graphicView = GraphicView.Front;

        [ObservableProperty] ElementInfo selectedItem;
        [ObservableProperty] IEnumerable<ElementInfo> elements;
        public EditorsOverviewPageViewModel()
        {
            Elements = ElementsSources.Elements.Take(new Range(15, 25));
            SelectedItem = Elements.ElementAt(3);
        }
        [RelayCommand]
        public void ShowPage(string parameter)
        {
            try
            {
                Process.Start(new ProcessStartInfo(parameter) { UseShellExecute = true });
            }
            catch { };
        }
    }
}
