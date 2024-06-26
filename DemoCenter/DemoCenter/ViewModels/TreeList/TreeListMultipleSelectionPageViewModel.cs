using DemoCenter.DemoData;
using System.Collections.ObjectModel;

namespace DemoCenter.ViewModels
{
    public partial class TreeListMultipleSelectionPageViewModel : PageViewModelBase
    {
        public TreeListMultipleSelectionPageViewModel()
        {
            Tasks = ProjectTasksGenerator.Generate();
            SelectedTasks = new(Tasks[0].Tasks.Take(5));
        }

        public List<ProjectTask> Tasks { get; }

        public ObservableCollection<ProjectTask> SelectedTasks { get; }

    }
}
