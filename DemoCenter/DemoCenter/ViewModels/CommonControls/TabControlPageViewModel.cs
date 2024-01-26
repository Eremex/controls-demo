using CommunityToolkit.Mvvm.ComponentModel;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls.Editors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls;

namespace DemoCenter.ViewModels
{
    public partial class TabControlPageViewModel : PageViewModelBase
    {
        private static readonly Random rnd = new(DateTime.Now.Millisecond);
        private int newCarCount;
        private readonly ObservableCollection<CarInfo> cars = new(CsvSources.Cars);

        [ObservableProperty] private TabStripLayoutType layoutType = TabStripLayoutType.Stretch;
        [ObservableProperty] private Dock placement = Dock.Top;
        [ObservableProperty] private TabDragMode dragMode = TabDragMode.Reorder;
        [ObservableProperty] private TabHeaderOrientation headerOrientation;

        [ObservableProperty]
        private TabControlCloseButtonShowMode closeButtonShowMode = TabControlCloseButtonShowMode.None;

        [ObservableProperty] private TabControlNewButtonShowMode newButtonShowMode = TabControlNewButtonShowMode.None;

        [ObservableProperty] private CarInfo selectedCar;

        [ObservableProperty] private bool isTabPanelVisible = true;

        public IEnumerable<CarInfo> Cars => cars;

        public IEnumerable<TabControlCloseButtonShowMode> CloseButtonShowModes { get; } = new[]
        {
            TabControlCloseButtonShowMode.None,
            TabControlCloseButtonShowMode.InActiveTab,
            TabControlCloseButtonShowMode.InAllTabs,
            TabControlCloseButtonShowMode.InHeaderPanel,
            TabControlCloseButtonShowMode.InAllTabs | TabControlCloseButtonShowMode.InHeaderPanel
        };

        [RelayCommand]
        private void OnNew()
        {
            var randomCar = CsvSources.Cars[rnd.Next(CsvSources.Cars.Count - 1)];
            var newCar = new CarInfo($"New Car ({++newCarCount})", randomCar);
            cars.Add(newCar);

            SelectedCar = newCar;
        }
        
        [RelayCommand]
        private void OnClose(object parameter)
        {
            if (parameter is CarInfo car)
            {
                cars.Remove(car);
            }
        }
    }
}