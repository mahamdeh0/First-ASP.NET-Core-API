using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace WebApplication5.Filter
{
    public class LogactivityFilter : IActionFilter
    {
        private readonly ILogger<LogactivityFilter> logger;

        public LogactivityFilter(ILogger<LogactivityFilter> logger)
        {
            this.logger = logger;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName} on controller {context.Controller} with arguments {JsonSerializer.Serialize(context.ActionArguments)}");
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation($" Action {context.ActionDescriptor.DisplayName} Executed on controller {context.Controller}");
        }


    }
}
