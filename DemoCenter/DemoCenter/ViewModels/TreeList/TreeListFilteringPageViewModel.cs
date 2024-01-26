using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class TreeListFilteringPageViewModel : PageViewModelBase
    {
        public TreeListFilteringPageViewModel()
        {
            Tasks = ProjectTasksGenerator.Generate();
        }

        public List<ProjectTask> Tasks { get; }
    }
}
