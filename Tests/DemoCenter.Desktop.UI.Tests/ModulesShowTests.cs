using Avalonia.Controls;
using Avalonia.Threading;
using DemoCenter.ViewModels;
using DemoCenter.Views;
using Eremex.Avalonia.TestUtls;
using Metsys.Bson;

namespace DemoCenter.Desktop.UI.Tests;
public class WindowsTests
{
    public WindowsTests()
    {
        MouseEventsHelper = new TestMouseEventsHelper();
    }

    protected async Task WaitEx(int ms)
    {
        Dispatcher.UIThread.RunJobs();
        await Task.Delay(ms);
    }

    protected TestMouseEventsHelper MouseEventsHelper { get; }
    protected void SendLeftMouseDown(Control control)
    {
        MouseEventsHelper.SendLeftMouseDown(control);
    }

    protected void SendLeftMouseUp(Control control)
    {
        MouseEventsHelper.SendLeftMouseUp(control);
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
    public async Task ShowAndClickDemoWindow()
    {
        var window = new MainWindow
        {
            DataContext = new MainViewModel()
        };
        window.Show();
        window.WindowState = Avalonia.Controls.WindowState.Maximized;
        await WaitEx(1000);
        SendLeftMouseDown(window);
        SendLeftMouseUp(window);

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