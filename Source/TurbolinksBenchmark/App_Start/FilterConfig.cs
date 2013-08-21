namespace TurbolinksBenchmark
{
    using System.Web.Mvc;
    using TurbolinksBenchmark.ActionFilters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TurbolinksAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}