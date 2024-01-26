using Avalonia.Controls;
using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls.TreeList;

namespace DemoCenter.Views
{
    public partial class TreeListDataEditorsPageView : UserControl
    {
        public TreeListDataEditorsPageView()
        {
            InitializeComponent();
        }

        private void OnShowingEditor(object sender, TreeListShowingEditorEventArgs e)
        {
            if ((e.Column.FieldName == nameof(ProjectTask.Status) || e.Column.FieldName == nameof(ProjectTask.EstimateTime)) && e.Node.Content is ProjectTask task && task.HasTasks)
                e.Cancel = true;
        }
    }
}
