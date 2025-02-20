using Newtonsoft.Json;
using System.Net;

namespace Enterprise_Expense_Tracker.Helper
{
    public class Log_Error_HandlerMiddleware
    {
        public RequestDelegate requestDelegate;
        private readonly ILogger<Log_Error_HandlerMiddleware> logger;
        public Log_Error_HandlerMiddleware
        (RequestDelegate requestDelegate, ILogger<Log_Error_HandlerMiddleware> logger)
        {
            this.requestDelegate = requestDelegate;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private Task HandleException(HttpContext context, Exception ex)
        {
            logger.LogError(ex.ToString());
            var errorMessageObject =
                new { Message = ex.Message, Code = "system_error" };

            var errorMessage = JsonConvert.SerializeObject(errorMessageObject);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
