namespace TurbolinksTestApp.Models
{
    using System.Collections.Generic;

    public class TaskEditModel
    {
        public string Title { get; set; }

        public string DomId { get; set; }

        public IEnumerable<Task> List { get; set; } 
    }
}