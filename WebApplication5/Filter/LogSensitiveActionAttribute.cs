using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace WebApplication5.Filter
{
    public class LogSensitiveActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Debug.WriteLine("Sensitive Action Executed !!!!!!!!!!!!");
        }
    }
}
