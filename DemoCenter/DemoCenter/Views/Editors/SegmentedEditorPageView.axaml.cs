using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

using DemoCenter.DemoData;
using DemoCenter.ViewModels;

namespace DemoCenter.Views
{
    public partial class SegmentedEditorPageView : UserControl
    {
        SegmentedEditorPageViewModel ViewModel { get; set; }
        public SegmentedEditorPageView()
        {
            InitializeComponent();
        }

        protected override void OnDataContextChanged(EventArgs e)
        {
            base.OnDataContextChanged(e);
            UnsubscribeEvents(ViewModel);
            ViewModel = DataContext as SegmentedEditorPageViewModel;
            SubscribeEvents(ViewModel);
        }

        private void SubscribeEvents(SegmentedEditorPageViewModel viewModel)
        {
            if (viewModel == null)
                return;
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void UnsubscribeEvents(SegmentedEditorPageViewModel viewModel)
        {
            if (viewModel == null)
                return;
            viewModel.PropertyChanged -= OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedViewType" && ViewModel?.SelectedViewType != null)
            {
                var type = ViewModel?.SelectedViewType.ToLower();
                if(type == "primary")
                {
                    ViewTypesSelector.Classes.Remove("secondary");
                    HorizontalAlignmentSelector.Classes.Remove("secondary");
                    GraphicViewSelector.Classes.Remove("secondary");
                } else if (type == "secondary" && !ViewTypesSelector.Classes.Contains("secondary"))
                {
                    ViewTypesSelector.Classes.Add("secondary");
                    HorizontalAlignmentSelector.Classes.Add("secondary");
                    GraphicViewSelector.Classes.Add("secondary");
                }
            }
        }
    }

    public class SegmentedEditorViewBackgroundConverter : MarkupExtension, IMultiValueConverter
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
        public object Convert(IList<object> values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[0] == AvaloniaProperty.UnsetValue || values.Count != 3)
                return null;
            var type = (string)values[0];
            return (string.Equals(type, "secondary", StringComparison.InvariantCultureIgnoreCase)) ? values[2] : values[1];
        }
    }
}
