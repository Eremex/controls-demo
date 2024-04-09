﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.ViewModels
{
    public partial class DataGridLargeDataViewModel : PageViewModelBase
    {
        [ObservableProperty]
        IList<OrderData> orders;

        [ObservableProperty]
        ItemsSourceSize selectedItemsSourceSize;

        public DataGridLargeDataViewModel()
        {
            SelectedItemsSourceSize = ItemsSourceSize.Medium;
            Generate();
        }

        [RelayCommand]
        void Generate() => Orders = OrderData.GenerateData(GetSourceSize());

        int GetSourceSize()
        {
            switch (SelectedItemsSourceSize)
            {
                case ItemsSourceSize.Small:
                    return 100000;
                case ItemsSourceSize.Medium:
                    return 500000;
                default:
                    return 1000000;
            }
        }
    }

    public enum ItemsSourceSize
    {
        Small,
        Medium,
        Large
    }
}