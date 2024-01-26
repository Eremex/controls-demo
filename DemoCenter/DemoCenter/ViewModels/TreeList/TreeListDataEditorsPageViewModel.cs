using DemoCenter.DemoData;

namespace DemoCenter.ViewModels
{
    public partial class TreeListDataEditorsPageViewModel : PageViewModelBase
    {
        public TreeListDataEditorsPageViewModel()
        {
            Tasks = ProjectTasksGenerator.Generate();
        }

        public List<ProjectTask> Tasks { get; }
    }
}
