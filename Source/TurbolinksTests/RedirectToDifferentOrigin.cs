namespace TurbolinksTests
{
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using NSubstitute;

    using TurbolinksTestApp.ActionFilters;

    using Xunit;

    public class RedirectToDifferentOrigin
    {
        private readonly ActionExecutedContext filterContext;
        private readonly HttpCookieCollection responseCookies;

        public RedirectToDifferentOrigin()
        {
            var httpContext = Substitute.For<HttpContextBase>();

            httpContext.Request.HttpMethod.Returns("GET");
            httpContext.Request.Headers.Returns(
                new NameValueCollection
                    {
                        {
                            "X-XHR-Referer", 
                            "http://localhost:61049/Projects/Create"
                        }
                    });

            responseCookies = new HttpCookieCollection();
            httpContext.Response.Cookies.Returns(responseCookies);

            var controllerContext = new ControllerContext(
                httpContext,
                new RouteData(),
                Substitute.For<ControllerBase>());

            filterContext = new ActionExecutedContext(
                controllerContext,
                Substitute.For<ActionDescriptor>(),
                false,
                null) { Result = new RedirectResult("http://google.com") };

            var attribute = new TurbolinksAttribute();

            attribute.OnActionExecuted(filterContext);
        }

        [Fact]
        public void SetsRequestMethodCookie()
        {
            Assert.Equal("GET", responseCookies["request_method"].Value);
        }

        [Fact]
        public void IssuesHttpForbidden()
        {
            var actionResult = (HttpStatusCodeResult)filterContext.Result;

            Assert.Equal(403, actionResult.StatusCode);
        }
    }
}