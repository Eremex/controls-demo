using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;
using DemoCenter.DemoData;
using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.TreeList;
using System.Collections;
using System.Globalization;

namespace DemoCenter.Views
{
    public partial class TreeListFilteringPageView : UserControl
    {
        public TreeListFilteringPageView()
        {
            InitializeComponent();
        }
    }

    public class ProjectTaskImageSelector : ITreeListNodeImageSelector
    {
        public IImage ProjectImage { get; set; }
        public IImage TaskImage { get; set; }

        public IImage SelectImage(TreeListNode node)
        {
            if(node.Content is ProjectTask task && task.HasTasks)
                return ProjectImage;
            return TaskImage;
        }
    }
}
