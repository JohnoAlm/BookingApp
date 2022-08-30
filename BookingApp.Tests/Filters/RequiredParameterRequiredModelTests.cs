using Booking.Web.Filters;
using BookingApp.Web.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Tests.Filters
{
    public class RequiredParameterRequiredModelTests
    {
        [Fact]
        public void OnActionExcecuting_IdHasNoValue_ShouldReturnBadREquest()
        {
            ActionExecutingContext context = GetContext<object>("id", null);
            var filter = new RequiredParameterRequiredModel("id");

            filter.OnActionExecuting(context);

            var result = context.Result;

            Assert.IsType<BadRequestResult>(result);
        }

        private ActionExecutingContext GetContext<T>(string key, T? value)
        {
            var routeValues = new RouteValueDictionary();
            routeValues.Add(key, value);

            var routeData = new RouteData(routeValues);

            var actionContext = new ActionContext(
                Mock.Of<HttpContext>(),
                routeData,
                Mock.Of<ActionDescriptor>()
                );

            var controller = new Mock<GymClassesController>();

            var actionExcecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                routeValues,
                controller);

            return actionExcecutingContext;
        }
    }
}
