namespace TurboLinksTest.Models
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        public DataContext() : base("DefaultConnection")
        {
        }

        public IDbSet<Project> Projects { get; set; }

        public IDbSet<Task> Tasks { get; set; }
    }
}