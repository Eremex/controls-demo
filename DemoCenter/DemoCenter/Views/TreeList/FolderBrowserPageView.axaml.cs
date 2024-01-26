using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.TreeList;
using System.Collections;

namespace DemoCenter.Views
{
    public partial class FolderBrowserPageView : UserControl
    {
        public FolderBrowserPageView()
        {
            InitializeComponent();
        }

        protected override void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);
            if (folderBrowserTreeList.Nodes.Count > 0)
                folderBrowserTreeList.Nodes[0].IsExpanded = true;
        }
    }

    public class FileSystemItemChildrenSelector : ITreeListChildrenSelector
    {
        public IEnumerable SelectChildren(object item)
        {
            if (item is FolderFileSystemItem folderItem)
                return folderItem.Items;
            return null;
        }

        public bool HasChildren(object item)
        {
            if (item is FolderFileSystemItem)
                return true;
            return false;
        }
    }

    public class FileSystemImageSelector : ITreeListNodeImageSelector
    {
        public IImage OpenedFolderImage { get; set; }
        public IImage ClosedFolderImage { get; set; }
        public IImage FileImage { get; set; }

        public IImage SelectImage(TreeListNode node)
        {
            if(node.Content is FolderFileSystemItem)
                return node.IsExpanded && node.HasChildren ? OpenedFolderImage : ClosedFolderImage;
            return FileImage;
        }
    }
}
