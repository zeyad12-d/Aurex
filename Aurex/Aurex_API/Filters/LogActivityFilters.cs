using Microsoft.AspNetCore.Mvc.Filters;

public class LogActivityFilter : IAsyncActionFilter
{
    private readonly ILogger<LogActivityFilter> _logger;

    public LogActivityFilter(ILogger<LogActivityFilter> logger)
    {
        _logger = logger;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        _logger.LogInformation("Started executing {ActionName} at {Time}", context.ActionDescriptor.DisplayName, DateTime.UtcNow);

        var resultContext = await next();

        stopwatch.Stop();
        _logger.LogInformation("Finished executing {ActionName} in {ElapsedMilliseconds} ms",
            context.ActionDescriptor.DisplayName, stopwatch.ElapsedMilliseconds);
    }


}
