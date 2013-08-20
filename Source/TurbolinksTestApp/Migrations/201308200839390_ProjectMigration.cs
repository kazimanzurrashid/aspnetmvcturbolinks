namespace TurbolinksTestApp.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ProjectMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                {
                    Id = c.Int(false, true),
                    Name = c.String(false, 128)
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.Projects");
        }
    }
}