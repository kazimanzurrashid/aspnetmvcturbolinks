namespace TurboLinksTest.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class ProjectViewModel
    {
        public ProjectViewModel(Project project)
        {
            ProjectId = project.Id;
            ProjectName = project.Name;

            IncompletedTasks = project.Tasks
                .Where(t => !t.Completed)
                .OrderBy(t => t.Name);

            CompletedTasks = project.Tasks
                .Where(t => t.Completed)
                .OrderBy(t => t.Name);
        }

        public int ProjectId { get; private set; }

        public string ProjectName { get; private set; }

        public IEnumerable<Task> IncompletedTasks { get; private set; } 

        public IEnumerable<Task> CompletedTasks { get; private set; } 
    }
}