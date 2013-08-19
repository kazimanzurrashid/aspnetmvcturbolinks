namespace TurboLinksTest.ActionFilters
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class TurbolinksAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Do nothing
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var request = filterContext.HttpContext.Request;
            var referrerUrl = request.Headers["X-XHR-Referer"];

            if (string.IsNullOrWhiteSpace(referrerUrl))
            {
                return;
            }

            var response = filterContext.HttpContext.Response;
            response.Cookies.Add(
                new HttpCookie("request_method", request.HttpMethod));

            string redirectUrl = null;
            var redirectResult = filterContext.Result as RedirectResult;

            if (redirectResult != null)
            {
                redirectUrl = redirectResult.Url;
            }
            else
            {
                var redirectToRouteResult = filterContext.Result as RedirectToRouteResult;

                if (redirectToRouteResult != null)
                {
                    redirectUrl = UrlHelper.GenerateUrl(
                        redirectToRouteResult.RouteName,
                        null,
                        null,
                        redirectToRouteResult.RouteValues,
                        RouteTable.Routes,
                        filterContext.RequestContext,
                        false);
                }
            }

            var session = filterContext.HttpContext.Session;

            if (string.IsNullOrWhiteSpace(redirectUrl))
            {
                if ((session != null) &&
                    (session["_turbolinks_redirect_to"] != null))
                {
                    redirectUrl = (string)session["_turbolinks_redirect_to"];

                    response.Headers["X-XHR-Redirected-To"] = redirectUrl;
                    session.Remove("_turbolinks_redirect_to");
                }
            }
            else
            {
                if (session != null)
                {
                    session["_turbolinks_redirect_to"] = redirectUrl;
                }

                if (!IsSameOrigin(redirectUrl, referrerUrl))
                {
                    filterContext.Result = new HttpStatusCodeResult(403);
                }
            }
        }

        private static bool IsSameOrigin(string redirectUrl, string referrerUrl)
        {
            if (string.IsNullOrWhiteSpace(redirectUrl))
            {
                return true;
            }

            var redirectUri = new UriBuilder(redirectUrl);
            var referrerUri = new UriBuilder(referrerUrl);

            return redirectUri.Scheme.Equals(
                referrerUri.Scheme,
                StringComparison.OrdinalIgnoreCase) &&
                redirectUri.Host.Equals(
                referrerUri.Host,
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
