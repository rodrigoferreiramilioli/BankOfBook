using Microsoft.AspNetCore.Builder;
using Microsoft.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace BankOfBook.Domain.Extensions
{
    public class ExceptionMiddleware
    {
        #region Fields

        private readonly RequestDelegate _next;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private const int ReadChunkBufferLength = 4096;

        #endregion

        #region Constructors

        public ExceptionMiddleware(RequestDelegate next)
        {
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        #endregion

        #region Methods

        public async Task Invoke(HttpContext context)
        {
            await Process(context);
        }

        private async Task Process(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await SetReturnModel(context, exception, Guid.NewGuid().ToString());
            }
        }

        private async Task<string> SetReturnModel(HttpContext context, Exception exception, string requestId)
        {
            var errorResponseModel = TreatErrorResponse(exception, requestId, out var statusCode);

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = context.Request.ContentType;

       
                var teste = JsonConvert.SerializeObject(
                    new
                    {
                        errorResponseModel.RequestId,
                        errorResponseModel.Message,
                        errorResponseModel.Code,
                        errorResponseModel.Errors
                    });

                await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(teste));

            return JsonConvert.SerializeObject(errorResponseModel);
        }

        private static ErrorResponseModel TreatErrorResponse(Exception exception, string requestId, out HttpStatusCode statusCode)
        {
            if (exception is WebException webException)
            {
                var restResponse = (HttpWebResponse)webException.Response;

                switch (restResponse.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                    case HttpStatusCode.InternalServerError:
                        using (var stream = restResponse.GetResponseStream())
                        {
                            try
                            {
                                statusCode = HttpStatusCode.BadRequest;

                                var reader = new StreamReader(stream, Encoding.UTF8);
                                var errorResponse =
                                    JsonConvert.DeserializeObject<ErrorResponseModel>(reader.ReadToEnd());

                                errorResponse.Code = "400";
                                return errorResponse;
                            }
                            catch (Exception)
                            {
                                statusCode = HttpStatusCode.InternalServerError;

                                return new ErrorResponseModel
                                {
                                    Code = "500",
                                    RequestId = requestId,
                                    Message = "Internal server error"
                                };
                            }
                        }
                    default:
                        statusCode = HttpStatusCode.InternalServerError;

                        return new ErrorResponseModel
                        {
                            Code = "500",
                            RequestId = requestId,
                            Message = "Internal server error"
                        };
                }
            }

            switch (exception)
            {
                case BusinessException _:
                    statusCode = HttpStatusCode.InternalServerError;
                    return new ErrorResponseModel
                    {
                        RequestId = requestId,
                        Code = "409",
                        Message = exception.Message,
                        Errors = GetErrors(exception.Data)
                    };
                default:
                    statusCode = HttpStatusCode.InternalServerError;

                    return new ErrorResponseModel
                    {
                        RequestId = requestId,
                        Code = "500",
                        Message = "Erro ao processar"
                    };
            }
        }

        private static List<Error> GetErrors(IDictionary data)
        {
            if (data == null) return null;

            var errors = new List<Error>();

            foreach (DictionaryEntry item in data)
                errors.Add(new Error { Code = item.Key.ToString(), Description = item.Value?.ToString() });

            return errors;
        }
        #endregion
    }

    public static class ExceptionMiddlewareGlobal
    {
        public static IApplicationBuilder UseExceptionGlobal(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}