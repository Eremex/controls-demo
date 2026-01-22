using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia.VisualTree;
using Eremex.AvaloniaUI.Controls;
using Eremex.AvaloniaUI.Controls.Common;
using Eremex.AvaloniaUI.Controls.DataControl;
using Eremex.DocumentProcessing.Exports;

namespace DemoCenter.Helpers
{
    public static class DemoExportHelper
    {
        public static void Export<C, T>(C control, T options, Action<Stream, C, T> action) where T : ExportOptions where C : Control
        {
            var owner = control.GetVisualRoot() as Window;

            try
            {
                var exportProgressControl = new ExportProgressControl();

                MxWindow window = CreateProgressWindow();
                window.Content = exportProgressControl;
                window.Show(owner);

                var directory = CreateTempDirectory();
                string tmpFileName = Path.Combine(directory, typeof(C).Name + GetFileExtension());
                using FileStream fileStream = new FileStream(tmpFileName, FileMode.Create);

                options.ExportProgress += OnExportProgress;
                action(fileStream, control, options);
                options.ExportProgress -= OnExportProgress;

                window.Close();

                if (MxMessageBox.Show(owner, Resources.ExportProgressPromptMessage, Resources.ExportProgressTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None) == MessageBoxResult.Yes)
                    OpenFile(tmpFileName);

                void OnExportProgress(object sender, ExportProgressEventArgs e)
                {
                    exportProgressControl.Value = e.ProgressPercentage;
                    Dispatcher.UIThread.RunJobs();
                }

            }
            catch (Exception e)
            {
                MxMessageBox.Show(owner, e.Message, "Error", MessageBoxButtons.Ok, MessageBoxIcon.Error);
            }

            string GetFileExtension()
            {
                return options switch { XlsxExportOptions => ".xlsx", PageExportOptions => ".pdf", _ => throw new NotImplementedException() };
            }
       }

        public static void ExportImage<C>(C control, ImageExportOptions options, Action<C, ImageExportOptions, string, string> exec) where C : Control
        {
            var owner = control.GetVisualRoot() as Window;

            try
            {
                var exportProgressControl = new ExportProgressControl();

                MxWindow window = CreateProgressWindow();
                window.Content = exportProgressControl;
                window.Show(owner);

                var directory = CreateTempDirectory();
                var fileFormat = GetFileFormat();
                var pageNumber = 1;

                options.PageRange = pageNumber.ToString();
                options.ExportProgress += OnExportProgress;
                exec(control, options, directory, fileFormat);
                options.ExportProgress -= OnExportProgress;

                window.Close();

                if (MxMessageBox.Show(owner, Resources.ExportProgressPromptMessage, Resources.ExportProgressTitle, MessageBoxButtons.YesNo, MessageBoxIcon.None) == MessageBoxResult.Yes)
                {
                    var fileName = Path.Combine(directory, string.Format(fileFormat, pageNumber));
                    OpenFile(fileName);
                }

                void OnExportProgress(object sender, ExportProgressEventArgs e)
                {
                    exportProgressControl.Value = e.ProgressPercentage;
                    Dispatcher.UIThread.RunJobs();
                }

            }
            catch (Exception e)
            {
                MxMessageBox.Show(owner, e.Message, "Error", MessageBoxButtons.Ok, MessageBoxIcon.Error);
            }

            string GetFileFormat()
            {
                return $"Page{{0:D2}}.{GetFileExtension(options.Format)}";
            }

            static string GetFileExtension(MxImageFormat format)
            {
                return Enum.GetName(typeof(MxImageFormat), format).ToLower();
            }
        }

        private static string CreateTempDirectory()
        {
            string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()).ToUpper();
            Directory.CreateDirectory(tempDir);
            return tempDir;
        }

        private static MxWindow CreateProgressWindow()
        {
            return new MxWindow()
            {
                Title = Resources.ExportProgressTitle,
                Width = 300,
                Height = 160,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                ShowInTaskbar = false,
                ShowCloseButton = false,
                ShowMinimizeButton = false,
                ShowMaximizeButton = false,
                CanResize = false
            };
        }

        private static void OpenFile(string fileName)
        {
            if (OperatingSystem.IsWindows())
                Process.Start(new ProcessStartInfo() { FileName = fileName, UseShellExecute = true });
            else if (OperatingSystem.IsLinux())
                Process.Start("xdg-open", fileName);
            else if (OperatingSystem.IsMacOS())
                Process.Start("open", fileName);
        }
    }
}
