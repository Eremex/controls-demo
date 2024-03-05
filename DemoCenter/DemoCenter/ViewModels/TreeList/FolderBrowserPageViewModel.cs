using CommunityToolkit.Mvvm.ComponentModel;

namespace DemoCenter.ViewModels
{
    public class FolderBrowserPageViewModel : PageViewModelBase
    {
        public FolderBrowserPageViewModel()
        {
            Drives = CreateDrives();
        }

        public List<DriveFileSystemItemModel> Drives { get; }

        private List<DriveFileSystemItemModel> CreateDrives()
        {
            List<DriveFileSystemItemModel> drives = new();
            Array.ForEach(Directory.GetLogicalDrives(), drive => drives.Add(new DriveFileSystemItemModel(drive)));
            return drives;
        }
    }

    public class DriveFileSystemItemModel : FolderFileSystemItem
    {
        public DriveFileSystemItemModel(string driveName) : base(driveName, driveName, FileSystemItem.DriveType, $"<{FileSystemItem.DriveType}>") { }
    }

    public class FolderFileSystemItem : FileSystemItem
    {
        private List<FileSystemItem> items;
        public FolderFileSystemItem(string name, string fullName, string type, string size) : base(name, fullName, type, size) { }

        public List<FileSystemItem> Items
        {
            get
            {
                if (items == null)
                    items = CreateItems();
                return items;
            }
        }

        private List<FileSystemItem> CreateItems()
        {
            var items = new List<FileSystemItem>();
            CreateFolderItems(items);
            CreateFileItems(items);
            return items;
        }

        private void CreateFileItems(IList<FileSystemItem> items)
        {
            try
            {
                var files = Directory.GetFiles(FullName);
                foreach (var file in files)
                {
                    try
                    {
                        items.Add(new FileSystemItem(Path.GetFileName(file), file, FileSystemItem.FileType, GetFileSize(file)));
                    }
                    catch { }
                }
            }
            catch { }

            string GetFileSize(string fullName) => FileSizeHelper.FileSizeToString(new FileInfo(fullName).Length);
        }

        private void CreateFolderItems(IList<FileSystemItem> items)
        {
            try
            {
                var directories = Directory.GetDirectories(FullName);
                foreach (var directory in directories)
                {
                    try
                    {
                        items.Add(new FolderFileSystemItem(GetDirectoryName(directory), directory, FileSystemItem.FolderType, $"<{FileSystemItem.FolderType}>"));
                    }
                    catch { }
                }
            }
            catch { }

            string GetDirectoryName(string fullName) => new DirectoryInfo(fullName).Name;
        }
    }

    public class FileSystemItem
    {
        public const string FileType = "File", FolderType = "Folder", DriveType = "Drive";

        public FileSystemItem(string name, string fullName, string type, string size)
        {
            Name = name;
            FullName = fullName;
            Type = type;
            Size = size;
        }
        public string Name { get; }
        public string FullName { get; }
        public string Type { get; }
        public string Size { get; }           
    }

    internal static class FileSizeHelper
    {
        static readonly long BytesInKilobyte = 1024, BytesInMegabyte = BytesInKilobyte * 1024, BytesInGigabyte = BytesInMegabyte * 1024;

        public static string FileSizeToString(long size)
        {
            if (size > BytesInGigabyte)
                return $"{size / BytesInGigabyte:0.##} GB";
            else if (size > BytesInMegabyte)
                return $"{size / BytesInMegabyte:0.##} MB";
            else if (size > BytesInKilobyte)
                return $"{size / BytesInKilobyte:0.##} KB";
            else
                return size > 0 ? $"1 KB" : $"0 KB";
        }
    }

}
