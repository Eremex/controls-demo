using Avalonia.Threading;
using DemoCenter.ViewModels;
using DemoCenter.Views;

namespace DemoCenter.Desktop.UI.Tests;
public class WindowsTests
{
    protected async Task WaitEx(int ms)
    {
        Dispatcher.UIThread.RunJobs();
        await Task.Delay(ms);
    }

    [Fact]
    public async Task ShowDemoWindow()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };
        window.Show();
        window.WindowState = Avalonia.Controls.WindowState.Maximized;
        await WaitEx(1000);
        window.Close();
    }
    [Fact]
    public async Task ShowAllModules()
    {
        MainViewModel mainViewModel = new MainViewModel();
        var window = new MainWindow
        {

            DataContext = mainViewModel
        };
        window.Show();
        window.WindowState = Avalonia.Controls.WindowState.Maximized;
        await WaitEx(100);

        foreach (var product in mainViewModel.FlatProducts) 
        {
            try
            {
                mainViewModel.SelectProduct(product.Name);
                await WaitEx(100);
            }
            catch (Exception ex)
            {
                    Assert.Fail(string.Format("problem in {0} {1}", product.Name, ex));
            }
        }

        window.Close();
    }
}