namespace TurbolinksTests
{
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using NSubstitute;

    using TurbolinksTestApp.ActionFilters;

    using Xunit;

    public class NoRedirection
    {
        [Fact]
        public void OnlySetsRequestMethodCookie()
        {
            var responseCookies = new HttpCookieCollection();

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

            httpContext.Response.Cookies.Returns(responseCookies);

            var controllerContext = new ControllerContext(
                httpContext,
                new RouteData(),
                Substitute.For<ControllerBase>());

            var filterContext = new ActionExecutedContext(
                controllerContext,
                Substitute.For<ActionDescriptor>(),
                false,
                null);

            var attribute = new TurbolinksAttribute();

            attribute.OnActionExecuted(filterContext);

            Assert.Equal("GET", responseCookies["request_method"].Value);
        }
    }
}