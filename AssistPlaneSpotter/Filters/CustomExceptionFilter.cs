using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace AssistPlaneSpotter.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Log.Error($"{filterContext.Exception.Message}|{filterContext.Exception.StackTrace}");
            filterContext.ExceptionHandled = true;
        }
    }
}
