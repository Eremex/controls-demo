using Avalonia.Controls;
using DemoCenter.ViewModels;

namespace DemoCenter.Views;

public partial class CartesianEmptyPointsView : UserControl
{
    CartesianEmptyPointsViewModel ViewModel => DataContext as CartesianEmptyPointsViewModel;
    
    public CartesianEmptyPointsView()
    {
        InitializeComponent();
    }
    void ViewOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ViewModel is not null && sender is ListBox { SelectedItems.Count: > 0 } listBox && listBox.SelectedItems[0] is ViewViewModel viewModel)
        {
            ViewModel.SelectedView = viewModel.View;
            ViewModel.DataAdapter = viewModel.DataAdapter;
        }
    }
}
