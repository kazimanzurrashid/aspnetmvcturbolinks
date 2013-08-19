namespace TurboLinksTest.Migrations
{
    using System.Data.Entity.Migrations;

    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataContext context)
        {
        }
    }
}