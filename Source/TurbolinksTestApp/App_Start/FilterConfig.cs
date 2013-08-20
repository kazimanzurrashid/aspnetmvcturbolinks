namespace TurbolinksTestApp
{
    using System.Web.Mvc;

    using ActionFilters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TurbolinksAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}