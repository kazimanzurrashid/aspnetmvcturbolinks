namespace TurbolinksTestApp.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TaskMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(false, 128),
                    Completed = c.Boolean(false),
                    ProjectId = c.Int(false)
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectId, true)
                .Index(t => t.ProjectId);
        }

        public override void Down()
        {
            DropIndex("dbo.Tasks", new[] { "ProjectId" });
            DropForeignKey("dbo.Tasks", "ProjectId", "dbo.Projects");
            DropTable("dbo.Tasks");
        }
    }
}