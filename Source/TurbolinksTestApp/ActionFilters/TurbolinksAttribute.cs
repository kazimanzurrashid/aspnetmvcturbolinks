namespace TurbolinksTestApp.ActionFilters
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class TurbolinksAttribute : FilterAttribute, IActionFilter
    {
        public virtual void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Do nothing
        }

        public virtual void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var request = filterContext.HttpContext.Request;
            var referrerUrl = request.Headers["X-XHR-Referer"];

            // If there is no xhr-Referer then this request is not
            // initiated by turblinks, so exit right away
            if (string.IsNullOrWhiteSpace(referrerUrl))
            {
                return;
            }

            var response = filterContext.HttpContext.Response;

            // Set the request method same as current request method
            response.Cookies.Add(
                new HttpCookie("request_method", request.HttpMethod)
                    {
                        HttpOnly = false // Client can access it
                    });

            // There are only two known way to redirect in aspnetmvc
            // the action result must be either redirect result or
            // redirect to route result, for any other custom
            // redirection we wont going to handle
            // so lets get the url that we are redirecting
            string redirectUrl = null;
            var redirectResult = filterContext.Result as RedirectResult;

            if (redirectResult != null)
            {
                redirectUrl = redirectResult.Url;
            }
            else
            {
                var redirectToRouteResult = filterContext.Result 
                    as RedirectToRouteResult;

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

            // Redirect url is not set which means we are not redirecting,
            // so we can exit here
            if (redirectUrl == null)
            {
                return;
            }

            // Now check if we are redirecting to the same origin,
            // if not then replace the action result with http forbidden
            // and exit
            if (!IsSameOrigin(redirectUrl, referrerUrl))
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                return;
            }

            // otherwise set the redirect to header
            response.Headers["X-XHR-Redirected-To"] = redirectUrl;
        }

        private static bool IsSameOrigin(string redirectUrl, string referrerUrl)
        {
            var redirectUri = new Uri(redirectUrl, UriKind.RelativeOrAbsolute);
            var referrerUri = new Uri(referrerUrl, UriKind.Absolute);

            if (redirectUri.IsAbsoluteUri)
            {
                return redirectUri.Scheme.Equals(
                    referrerUri.Scheme,
                    StringComparison.OrdinalIgnoreCase) &&
                    redirectUri.Host.Equals(
                    referrerUri.Host,
                    StringComparison.OrdinalIgnoreCase) &&
                    redirectUri.Port.Equals(referrerUri.Port);
            }

            // TODO: Do we need to check for relative, maybe not?
            return true;
        }
    }
}
