using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace BankOfBook.Api.v1.Extensions
{
    public static class ControllerExtensions
    {
        public static List<T> CreateResponse<T>(this Controller controller, List<T> result, long total)
        {
            controller.Response.ContentType = controller.Request.GetContentType();
            controller.Response.Headers.Add("Access-Control-Expose-Headers", "X-Total-Count");
            controller.Response.Headers.Add("X-Total-Count", total.ToString());
            controller.Response.StatusCode = (int)HttpStatusCode.OK;

            return result;
        }

        public static T CreateResponse<T>(this Controller controller, T value)
        {
            if (value == null)
                controller.Response.StatusCode = (int)HttpStatusCode.NotFound;
            else
                controller.Response.StatusCode = (int)HttpStatusCode.OK;

            controller.Response.ContentType = controller.Request.GetContentType();

            return value;
        }

        public static T CreateResponse<T>(this Controller controller, HttpStatusCode statusCode, T value)
        {
            controller.Response.ContentType = controller.Request.GetContentType();
            controller.Response.StatusCode = (int)statusCode;

            return value;
        }

        public static void CreateResponse(this Controller controller, HttpStatusCode statusCode)
        {
            controller.Response.ContentType = controller.Request.GetContentType();
            controller.Response.StatusCode = (int)statusCode;
        }

        public static string GetContentType(this HttpRequest request)
        {
            return request.Headers.Keys.Contains("Accept")
                ? request.Headers["Accept"].ToString()
                : "application/json";
        }
    }
}