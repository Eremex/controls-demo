using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class ContextMenuPageViewModel : PageViewModelBase
    {
        [ObservableProperty] ObservableCollection<MechInfo> mechs;
        [ObservableProperty] bool orderByAscending;
        [ObservableProperty] bool orderByDescending;

        [ObservableProperty] DateTime dateTime;

        private Random random = new Random();

        public ContextMenuPageViewModel()
        {
            ReloadMechs();
            DateTime = DateTime.Now;
        }

        [RelayCommand]
        public void Reload() => ReloadMechs();

        [RelayCommand]
        public void ClearAllButTheFirst() => Mechs = new ObservableCollection<MechInfo> { Mechs.First() };

        void ReloadMechs(int count = 4)
        {
            var result = new ObservableCollection<MechInfo>();
            var mechsCount = CsvSources.Mechs.Count;

            do
            {
                var item = CsvSources.Mechs[random.Next(0, mechsCount)];
                if(!result.Contains(item))
                    result.Add(item);
            }
            while (result.Count < count);
            Mechs = result;
            if (OrderByAscending)
                CreateAscendingMechs();
            else if (OrderByDescending)
                CreateDescendingMechs();
        }

        partial void OnOrderByAscendingChanged(bool value)
        {
            if (value) 
            { 
                CreateAscendingMechs();
                OrderByDescending = false;
            }
        }

        partial void OnOrderByDescendingChanged(bool value)
        {
            if (value)
            {
                CreateDescendingMechs();
                OrderByAscending = false;
            }                
        }

        private void CreateAscendingMechs() => Mechs = new ObservableCollection<MechInfo>(Mechs.OrderBy(x => x.Name));

        private void CreateDescendingMechs() => Mechs = new ObservableCollection<MechInfo>(Mechs.OrderByDescending(x => x.Name));
    }
}
