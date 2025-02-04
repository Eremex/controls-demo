using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls;

namespace DemoCenter.Views
{
    public partial class MessageBoxPageView : UserControl
    {
        public MessageBoxPageView()
        {
            InitializeComponent();
        }

        private void Button_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = (MessageBoxPageViewModel)DataContext;
            var res = MxMessageBox.Show(null, viewModel.Text, viewModel.Title, viewModel.Buttons, viewModel.Icon, viewModel.Result, 
                configure: msgBox =>
                {
                    msgBox.ButtonAlignment = viewModel.ButtonAlignment;
                });
        }
    }
}
