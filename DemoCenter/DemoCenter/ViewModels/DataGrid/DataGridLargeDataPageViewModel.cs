using Avalonia.Controls.Platform;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DemoCenter.DemoData;
using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace DemoCenter.ViewModels
{
    public partial class DataGridLargeDataViewModel : PageViewModelBase
    {
        [ObservableProperty]
        IEnumerable<LargeDataItem> items;

        [ObservableProperty]
        IEnumerable<LargeDataColumn> columns;

        [ObservableProperty]
        ItemsCount selectedItemsCount;

        [ObservableProperty]
        ItemsCount selectedColumnsCount;

        Random random = new Random();

        public DataGridLargeDataViewModel()
        {
            SelectedItemsCount = ItemsCount.Medium;
            SelectedColumnsCount = ItemsCount.Small;
            Generate();
        }

        [RelayCommand]
        void Generate()
        {
            Columns = Enumerable.Range(0, GetColumnsCount()).Select(x => GetColumn(x)).ToList();
            Items = Enumerable.Range(0, GetItemsCount()).Select(x => new LargeDataItem() { Id = x }).ToList();

            LargeDataColumn GetColumn(int index)
            {
                if (index == 0)
                    return new LargeDataColumn("Id", "Id (0)", typeof(int));

                int remainder = (index - 1) % 5;
                switch (remainder)
                {
                    case 1:
                        return new LargeDataColumn($"Numeric ({index})", typeof(int));
                    case 2:
                        return new LargeDataColumn($"ComboBox ({index})", typeof(string));
                    case 3:
                        return new LargeDataColumn($"DateTime ({index})", typeof(DateTime));
                    case 4:
                        return new LargeDataColumn($"CheckBox ({index})", typeof(bool));

                    default:
                        return new LargeDataColumn($"Text ({index})", typeof(string));
                }
            }
        }

        int GetItemsCount()
        {
            switch (SelectedItemsCount)
            {
                case ItemsCount.Small:
                    return 100000;
                case ItemsCount.Medium:
                    return 500000;
                default:
                    return 1000000;
            }
        }

        int GetColumnsCount()
        {
            switch (SelectedColumnsCount)
            {
                case ItemsCount.Small:
                    return 100;
                case ItemsCount.Medium:
                    return 500;
                default:
                    return 1000;
            }
        }
    }

    public enum ItemsCount
    {
        Small,
        Medium,
        Large
    }

    public class LargeDataItem
    {
        public int Id { get; set; }
    }

    public class LargeDataColumn
    {
        public LargeDataColumn(string fieldName, Type dataType)
        {
            FieldName = fieldName;
            Header = fieldName;
            DataType = dataType;
        }
        
        public LargeDataColumn(string fieldName, string header, Type dataType)
        {
            FieldName = fieldName;
            Header = header;
            DataType = dataType;
        }

        public string FieldName { get; private set; }

        public string Header { get; private set; }

        public Type DataType { get; private set; }
    }
}