using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace DemoCenter.DemoData
{
    public enum TaskStatus
    {
        [Display(Name = "Not Started")]
        NotStarted,
        [Display(Name = "In Progress")]
        InProgress,
        [Display(Name = "Completed")]
        Completed
    }

    public partial class ProjectTask : ObservableObject
    {
        public ProjectTask(ProjectTask parent, string description, TaskStatus status, int estimateTime, int timeSpent, string assignee, DateTime dueDate)
        {
            Tasks = new();
            Parent = parent;
            this.description = description;
            this.status = status;
            this.estimateTime = estimateTime;
            this.timeSpent = timeSpent;
            this.assignee = assignee;
            this.dueDate = dueDate;
        }

        public ProjectTask Parent { get; }

        [ObservableProperty]
        private bool highPriority;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private TaskStatus status;

        [ObservableProperty]
        private string assignee;

        [ObservableProperty]
        private int estimateTime;

        [ObservableProperty]
        private int timeSpent;

        [ObservableProperty]
        private int progress;

        [ObservableProperty]
        private DateTime dueDate;

        public List<ProjectTask> Tasks { get; }

        public bool HasTasks => Tasks.Count > 0;

        partial void OnStatusChanged(TaskStatus value) => Parent?.UpdateStatus();

        partial void OnEstimateTimeChanged(int value) => Parent?.UpdateEstimateTime();

        partial void OnTimeSpentChanged(int value) => Parent?.UpdateTimeSpent();

        private void UpdateStatus()
        {
            Status = Tasks.All(t => t.Status == TaskStatus.Completed) ? TaskStatus.Completed : Tasks.All(t => t.Status == TaskStatus.NotStarted) ? TaskStatus.NotStarted : TaskStatus.InProgress;
        }

        private void UpdateEstimateTime() => EstimateTime = Tasks.Sum(t => t.EstimateTime);  

        private void UpdateTimeSpent() => TimeSpent = Tasks.Sum(t => t.TimeSpent);

        private void UpdateProgress() => Progress = Tasks.Sum(t => t.Progress) / Tasks.Count;

        internal void Update()
        {
            UpdateStatus();
            UpdateEstimateTime();
            UpdateTimeSpent();
            UpdateProgress();
        }
    }

    public class ProjectTasksGenerator
    {
        private static readonly Random random = new();

        public static List<ProjectTask> Generate()
        {
            List<ProjectTask> tasks = new();

            CreateProject(tasks, "'Trade Central' Web Site", 0, WebSiteTaskNames, new DateTime(2023, 11, 16));
            CreateProject(tasks, "'World Wonders' Mobile App", WebSiteTaskNames.Length + 1, MobileAppTaskNames, new DateTime(2023, 11, 10));
            CreateProject(tasks, "'Six Sigma' Mobile App", WebSiteTaskNames.Length + 1, MobileAppTaskNames, new DateTime(2023, 10, 20));
            CreateProject(tasks, "'Profit First' Web Site", 0, WebSiteTaskNames,new DateTime(2023, 12, 11));

            return tasks;
        }

        private static void CreateProject(List<ProjectTask> items, string description, int assigneeIndex, string[] taskNames, DateTime dueDate)
        {
            var project = new ProjectTask(null, description, TaskStatus.InProgress, 0, 0, AssigneeNames[assigneeIndex], dueDate);                        
            GenerateProjectTasks(project, assigneeIndex + 1, taskNames);
            items.Add(project);
        }

        private static void GenerateProjectTasks(ProjectTask project, int assigneeIndex, string[] taskNames)
        {
            int completedCount = taskNames.Length / 3, inProgressAndCompletedCount = completedCount + taskNames.Length / 2;

            for (int i = 0; i < taskNames.Length; i++)
            {
                var status = i <= completedCount ? TaskStatus.Completed : (i <= inProgressAndCompletedCount ? TaskStatus.InProgress : TaskStatus.NotStarted);
                var dueDate = project.DueDate - new TimeSpan((taskNames.Length - i) * 5, i, i, 0);
                var estimate = random.Next(10, 70);
                var timeSpent = status == TaskStatus.Completed ? random.Next(estimate - 5, estimate + 5) : random.Next(10, estimate);

                var task = new ProjectTask(project, taskNames[i], status, estimate, timeSpent, AssigneeNames[assigneeIndex + i], dueDate);
                task.Progress = status == TaskStatus.Completed ? 100 : (status == TaskStatus.InProgress ? random.Next(10, 80) : 0);
                task.HighPriority = random.Next(1, taskNames.Length) == i;

                project.Tasks.Add(task);
            }

            project.Update();
        }

        private static readonly string[] WebSiteTaskNames = new[]
        {
           "Market research", "Define objectives and requirements", "Domain name registration",
           "Select a hosting provider", "Create a sitemap", "Wireframing and mockups",
           "Design UI/UX", "Front-end development", "Back-end development",
           "Content creation", "SEO (Search Engine Optimization)", "Mobile responsiveness",
           "Testing and quality assurance", "Security Assessment", "Launch and post-launch activities"
        };

        private static readonly string[] MobileAppTaskNames = new[]
        {
           "Market research", "Define objectives and requirements",
           "Wireframing and mockups", "Design UI/UX", "Front-end development",
           "Back-end development", "API Integration", "Deployment",
           "Testing and quality assurance", "Security Assessment", "Launch and post-launch activities",
        };

        private static readonly string[] AssigneeNames = new[]
        {
            "Ben Elliott", "Nelson Blackburn", "Clifford Hines", "Kaitlin Watts", "Lana Burnett",
            "Stanley Dorsey", "Tony Huffman", "Lisa Marquez", "Tim Robinson", "Jodie Bradley",
            "Casey Mccarthy", "Ralph Livingston", "Scott Reed", "Sarah Evans", "Jeremy Pearson", "Mattie Fowler",
            "Kylie Phillips", "Stefan Garrison", "Daniel Harvey", "Krish Joyce", "Kaitlyn Thomas", "Dan Berry",
            "John Oneal", "Nikolas Andrews", "Lisa Chan", "Molly Byrd", "Oliver Welsh", "Dillan Tanner"
        };
    }
}