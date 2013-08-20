namespace TurbolinksTests
{
    using System.Collections.Specialized;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using NSubstitute;

    using TurbolinksTestApp.ActionFilters;

    using Xunit;

    public class NoXhrRefererHeaderPresent
    {
        [Fact]
        public void DoesNothing()
        {
            var responseCookies = new HttpCookieCollection();
            var responseHeaders = new NameValueCollection();

            var httpContext = Substitute.For<HttpContextBase>();

            httpContext.Request.Headers.Returns(new NameValueCollection());

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
                null);

            var attribute = new TurbolinksAttribute();

            attribute.OnActionExecuted(filterContext);

            Assert.Empty(responseCookies);
            Assert.Empty(responseHeaders);
        }
    }
}