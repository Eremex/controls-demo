using Avalonia.Controls;
using DemoCenter.ViewModels;

namespace DemoCenter.Views;

public partial class WordPadExampleView : UserControl
{
    public WordPadExampleView()
    {
        InitializeComponent();
        DataContext = new WordPadExampleViewModel();
    }
}