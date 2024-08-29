using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Behaviors;

public class LoggingBehavior<TRequest, TReponse>
    (ILogger<LoggingBehavior<TRequest, TReponse>> _logger)
: IPipelineBehavior<TRequest, TReponse>
    where TRequest : notnull, IRequest<TReponse>
    where TReponse : notnull
{
    public async Task<TReponse> Handle(TRequest request, 
        RequestHandlerDelegate<TReponse> next, 
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("[Start] Handler request = {Request} - Response = {Response} , {RequestData}",
            typeof(TRequest).Name,typeof(TReponse).Name, request);

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeToken = timer.Elapsed;
        if(timeToken.Seconds > 3)
        {
            _logger.LogWarning("[PERFORMANCE] The request {Request} token {timeToken}",
            typeof(TRequest).Name, timeToken.Seconds);
        }

        _logger.LogInformation("[END] Handler request = {Request} with Response = {Response}",
             typeof(TRequest).Name, typeof(TReponse).Name);

        return response;
    }
}

