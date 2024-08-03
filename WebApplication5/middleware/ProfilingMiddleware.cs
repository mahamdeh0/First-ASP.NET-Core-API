using System.Diagnostics;

namespace WebApplication5.middleware
{
    public class ProfilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ProfilingMiddleware> _Logger;

        public ProfilingMiddleware(RequestDelegate next, ILogger<ProfilingMiddleware> logger)
        {
            this._next = next;
            _Logger = logger;
        }


        public async Task Invoke(HttpContext context)
        { 

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();
            _Logger.LogInformation($"request `{context.Request.Path}` took `{stopwatch.ElapsedMilliseconds}`to done");
                    
        }
    }
}

