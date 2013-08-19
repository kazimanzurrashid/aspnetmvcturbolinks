namespace $rootnamespace$.ActionFilters
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class TurboLinksAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var request = filterContext.HttpContext.Request;

            var referrer = request.Headers["X-XHR-Referer"];

            if (string.IsNullOrWhiteSpace(referrer))
            {
                return;
            }

            var response = filterContext.HttpContext.Response;

            response.Headers["X-XHR-Redirected-To"] = referrer;
            response.Cookies.Add(new HttpCookie("request_method", request.HttpMethod));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var redirectResult = filterContext.Result as RedirectResult;

            if (redirectResult == null)
            {
                return;
            }

            var redirectUrl = redirectResult.Url;

            var request = filterContext.HttpContext.Request;

            var referrerUrl = request.Headers["X-XHR-Referer"];

            if (!SameOrigin(redirectUrl, referrerUrl))
            {
                filterContext.Result = new HttpStatusCodeResult(403);
            }
        }

        private static bool SameOrigin(string redirectUrl, string referrerUrl)
        {
            var redirectUri = new UriBuilder(redirectUrl);

            if (string.IsNullOrWhiteSpace(redirectUrl))
            {
                return true;
            }

            var referrerUri = new UriBuilder(referrerUrl);

            if (!redirectUri.Scheme.Equals(
                referrerUri.Scheme,
                StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!redirectUri.Host.Equals(
                referrerUri.Host,
                StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}
