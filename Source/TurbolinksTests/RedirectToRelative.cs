namespace TurbolinksTests
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using NSubstitute;
    using Xunit;

    using TurbolinksTestApp.ActionFilters;

    public class RedirectToRelative : IDisposable
    {
        private readonly HttpCookieCollection responseCookies;
        private readonly NameValueCollection responseHeaders;

        public RedirectToRelative()
        {
            RouteTable.Routes.MapRoute(
                Guid.NewGuid().ToString(),
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                });

            var httpContext = Substitute.For<HttpContextBase>();

            httpContext.Request.HttpMethod.Returns("GET");
            httpContext.Request.Headers.Returns(
                new NameValueCollection
                    {
                        {
                            "X-XHR-Referer", 
                            "http://localhost:61049/Projects"
                        }
                    });

            responseCookies = new HttpCookieCollection();
            responseHeaders = new NameValueCollection();

            httpContext.Response.Cookies.Returns(responseCookies);
            httpContext.Response.Headers.Returns(responseHeaders);

            var controllerContext = new ControllerContext(
                httpContext,
                new RouteData(),
                Substitute.For<ControllerBase>());

            var filterContext = new ActionExecutedContext(
                controllerContext,
                Substitute.For<ActionDescriptor>(),
                false,
                null) { Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new Dictionary<string, object>())) };

            var attribute = new TurbolinksAttribute();

            attribute.OnActionExecuted(filterContext);
        }

        [Fact]
        public void SetsRequestMethodCookie()
        {
            Assert.Equal("GET", responseCookies["request_method"].Value);
        }

        [Fact]
        public void SetsXhrRedirectToResponseHeader()
        {
            Assert.NotNull(responseHeaders["X-XHR-Redirected-To"]);
        }

        public void Dispose()
        {
            RouteTable.Routes.RemoveAt(RouteTable.Routes.Count - 1);
        }
    }
}