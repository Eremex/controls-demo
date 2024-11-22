using Avalonia.Controls;
using DemoCenter.ViewModels.Ribbon;

namespace DemoCenter.Views.Ribbon;

public partial class WordPadExampleView : UserControl
{
    public WordPadExampleView()
    {
        InitializeComponent();
        DataContext = new WordPadExampleViewModel();
    }
}