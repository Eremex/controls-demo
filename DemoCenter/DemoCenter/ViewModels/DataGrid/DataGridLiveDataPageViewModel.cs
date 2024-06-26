using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DemoCenter.ViewModels
{
    public partial class DataGridLiveDataPageViewModel : PageViewModelBase
    {
        static IReadOnlyList<(string name, int count)> ProcessList { get; } = new List<(string name, int count)>()
        {
            ("DemoCenter.Desktop", 1), ("Microsoft Edge", 5), ("Microsoft Visual Studio 2022", 3), ("Windows Explorer", 4),
            ("Google Chrome", 5), ("Notepad", 3), ("Paint", 2), ("Desktop Window Manager", 1), ("Task Manager", 1),
            ("Calculator", 1), ("Mail", 1), ("Media Player", 1), ("Cortana", 1), ("Settings", 1),
            ("Delta Design", 1), ("Telegram Desktop", 1), ("System", 1), ("System Interrupts", 1), ("Runtime Broker", 10),
            ("COM Surrogate", 5), ("CTF Loader", 1), ("AggregatorHost", 1), ("Client Server Runtime Process", 1), ("Registry", 1),
            ("Microsoft IME", 1), ("Host Process for Window Tasks", 1), ("Application Frame Host", 1), ("Console Window Host", 3), ("Antimalware Core Service", 1),
            ("Shell Infrastructure Host", 1), ("Windows Start-Up Application", 1), ("Windows Session Manager", 1), ("Secure System", 1), ("Credential Guard & Key Guard", 1),
            ("Local Security Authority Process", 1), ("svchost", 30)
        };

        Random random = new Random();
        DispatcherTimer dispatcherTimer;

        [ObservableProperty]
        IList<ProcessInfo> processes;

        [ObservableProperty]
        double totalCpuLoad;

        [ObservableProperty]
        double totalMemoryLoad;

        [ObservableProperty]
        double totalDiskLoad;

        [ObservableProperty]
        double totalNetworkLoad;

        [ObservableProperty]
        double totalGpuLoad;

        public DataGridLiveDataPageViewModel()
        {
            var source = new List<ProcessInfo>();
            foreach (var process in ProcessList)
            {
                for (var i = 0; i < process.count; i++)
                    source.Add(new ProcessInfo() { Name = process.name });
            }
            Processes = new ObservableCollection<ProcessInfo>(source.OrderBy(x => random.Next()));
            UpdateProcesses();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(250);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        void UpdateProcesses()
        {
            UpdateValue(x => TotalCpuLoad = x, (p, x) => p.CpuLoad = x, 8, 40, 30);

            UpdateValue(x => TotalMemoryLoad = x, (p, x) => p.MemoryLoad = x, 20, 32 * 1024, 10 * 1024);

            UpdateValue(x => TotalDiskLoad = x, (p, x) => p.DiskLoad = x, 5, 10, 10);

            UpdateValue(x => TotalNetworkLoad = x, (p, x) => p.NetworkLoad = x, 5, 10, 1);

            UpdateValue(x => TotalGpuLoad = x, (p, x) => p.GpuLoad = x, 2, 10, 0);
        }

        public void RunUpdate()
        {
            dispatcherTimer.Start();
        }

        public void StopUpdate()
        {
            dispatcherTimer.Stop();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateProcesses();
        }

        void UpdateValue(Action<double> updateTotalValue, Action<ProcessInfo, double> updateProcessValue, int activeProcessCount, double maxActiveValue, double otherValue)
        {   
            var processes = Processes.ToList();
            var sum = 0d;

            maxActiveValue = maxActiveValue * random.Next(3) / 3;

            for (int i = 0; i < activeProcessCount; i++)
            {
                var index = random.Next(processes.Count);
                var value = Math.Round(maxActiveValue * random.NextDouble(), 1);
                maxActiveValue -= value;
                updateProcessValue(processes[index], value);
                sum += value;
                processes.RemoveAt(index);
            }

            foreach (var process in processes)
            {
                var value = Math.Round(otherValue / Processes.Count * random.NextDouble(), 1);
                updateProcessValue(process, value);
                sum += value;
            }

            updateTotalValue(sum);
        }
    }

    public partial class ProcessInfo : ObservableObject
    {
        [ObservableProperty]
        string name;

        [ObservableProperty]
        double cpuLoad;

        [ObservableProperty]
        double memoryLoad;

        [ObservableProperty]
        double diskLoad;

        [ObservableProperty]
        double networkLoad;

        [ObservableProperty]
        double gpuLoad;
    }
}
