using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BankOfBook.Api.v1.Extensions
{
    [Produces("application/json")]
    public abstract class ControllerBase : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptor;

        protected ControllerBase(IActionDescriptorCollectionProvider provider)
        {
            _actionDescriptor = provider;
        }

        [HttpOptions]
        public void Options()
        {
            var controllerRequested = ControllerContext.ActionDescriptor.ControllerName;
            var supportedMethods = _actionDescriptor.ActionDescriptors.Items
                .Where(d =>
                {
                    var controller = d.RouteValues["Controller"];
                    return string.Equals(
                        controller, controllerRequested, StringComparison.OrdinalIgnoreCase);
                })
                .Select(d =>
                    ((HttpMethodActionConstraint)d.ActionConstraints?.FirstOrDefault()!)?.HttpMethods.FirstOrDefault())
                .Distinct().ToList();

            supportedMethods.Remove("OPTIONS");

            var httpMethods = supportedMethods.ToArray();

            if (!httpMethods.Any())
                this.CreateResponse(HttpStatusCode.NotFound);

            if (Request.Headers.ContainsKey("Access-Control-Request-Method"))
            {
                if (!httpMethods.Contains(Request.Headers["Access-Control-Request-Method"].ToString()))
                {
                    Response.Headers.Add("Allow", string.Join(",", httpMethods));
                    this.CreateResponse(HttpStatusCode.MethodNotAllowed);

                    return;
                }
            }
            else
            {
                Response.Headers.Add("Access-Control-Allow-Methods", string.Join(",", httpMethods));
            }

            if (Request.Headers.ContainsKey("Access-Control-Request-Headers"))
                Response.Headers.Add("Access-Control-Allow-Headers", Request.Headers["Access-Control-Request-Headers"].ToString());

            if (!Response.Headers.Keys.Contains("Access-Control-Allow-Origin"))
                Response.Headers.Add("Access-Control-Allow-Origin", "*");

            this.CreateResponse(HttpStatusCode.OK);
        }
    }
}