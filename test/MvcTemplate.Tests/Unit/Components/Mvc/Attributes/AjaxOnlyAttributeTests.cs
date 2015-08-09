﻿using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using MvcTemplate.Components.Mvc;
using NSubstitute;
using System;
using Xunit;

namespace MvcTemplate.Tests.Unit.Components.Mvc
{
    public class AjaxOnlyAttributeTests
    {
        #region Method: IsValidForRequest(RouteContext routeContext, ActionDescriptor action)

        [Fact]
        public void IsValidForRequest_OnNullHeadersReturnsFalse()
        {
            RouteContext context = new RouteContext(HttpContextFactory.CreateHttpContext());
            context.HttpContext.Request.Headers.Returns(null as IHeaderDictionary);

            Assert.False(new AjaxOnlyAttribute().IsValidForRequest(context, null));
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("XMLHttpRequest", true)]
        public void IsValidForRequest_ValidatesAjaxRequests(String headerValue, Boolean expected)
        {
            RouteContext context = new RouteContext(HttpContextFactory.CreateHttpContext());
            context.HttpContext.Request.Headers["X-Requested-With"].Returns(headerValue);

            Boolean actual = new AjaxOnlyAttribute().IsValidForRequest(context, null);

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}