using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace DemoCenter.ViewModels
{
    public partial class ToolbarAndMenuPageViewModel : PageViewModelBase
    {
        [ObservableProperty] string message;
        [ObservableProperty] private bool isSyncing;
        [ObservableProperty] private string syncText = "Sync Completed";
        [ObservableProperty] private double syncProgress = 100;
        [ObservableProperty] private string text = "Text Editor (please type here)";
        [ObservableProperty] private bool autoSyncEnabled = true;

        public ToolbarAndMenuPageViewModel()
        {
            Message = GetType().FullName;
            timer =  new() { Interval = TimeSpan.FromMilliseconds(10) };
            timer.Tick += TimerOnTick;
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            SyncProgress = Math.Min(100, SyncProgress + 1);
            if(SyncProgress == 100)
            {
                IsSyncing = false;
            }
        }

        [RelayCommand]
        public void ToggleSync(object par)
        {
            IsSyncing = !IsSyncing;
        }

        partial void OnTextChanged(string value)
        {
            if(AutoSyncEnabled)
            {
                IsSyncing = true;
            }
        }
        
        partial void OnIsSyncingChanged(bool value)
        {
            if(IsSyncing)
            {
                SyncText = "Syncing...";
                StartSynchronization();
            }
            else
            {
                if(SyncProgress == 100)
                {
                    SyncText = "Sync Completed";   
                }
                else
                {
                    SyncText = "Click the circle to sync the document";
                }

                StopSynchronization();
            }
        }

        private readonly DispatcherTimer timer;
        private void StartSynchronization()
        {
            SyncProgress = 0;
            timer.Start();
        }

        private void StopSynchronization()
        {
            timer.Stop();
        }

        partial void OnAutoSyncEnabledChanged(bool value)
        {
            if (value)
            {
                IsSyncing = true;
            }
        }
    }
}
