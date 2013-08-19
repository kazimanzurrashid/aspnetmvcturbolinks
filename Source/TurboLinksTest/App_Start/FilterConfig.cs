namespace TurboLinksTest
{
    using System.Web.Mvc;

    using ActionFilters;

    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TurboLinksAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}