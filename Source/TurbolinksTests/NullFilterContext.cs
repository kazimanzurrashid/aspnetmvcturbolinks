namespace TurbolinksTests
{
    using System;

    using TurbolinksTestApp.ActionFilters;

    using Xunit;

    public class NullFilterContext
    {
        [Fact]
        public void ThrowsException()
        {
            var attribute = new TurbolinksAttribute();

            Assert.Throws<ArgumentNullException>(() =>
                attribute.OnActionExecuted(null));
        }
    }
}