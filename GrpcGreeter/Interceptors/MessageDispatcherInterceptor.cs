using Grpc.Core;
using Grpc.Core.Interceptors;
using MassTransit;

namespace GrpcGreeter.Interceptors;

public class MessageDispatcherInterceptor : Interceptor
{
    private readonly IPublishEndpoint publishEndpoint;
    private readonly ILogger<MessageDispatcherInterceptor> logger;

    public MessageDispatcherInterceptor(ILoggerFactory loggerFactory, IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
        logger = loggerFactory.CreateLogger<MessageDispatcherInterceptor>();
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            logger.LogInformation("Publishing Message");
            await this.publishEndpoint.Publish<TRequest>(request);
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error thrown by {Method}", context.Method);
            throw;
        }
    }
}