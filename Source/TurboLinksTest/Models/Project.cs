namespace TurboLinksTest.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Projects")]
    public class Project
    {
        private ICollection<Task> tasks;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Task> Tasks
        {
            get
            {
                return tasks ??
                    (tasks = new HashSet<Task>());
            }
        }
    }
}