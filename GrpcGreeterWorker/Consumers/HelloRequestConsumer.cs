using System.Threading.Tasks;
using GrpcGreeter;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace GrpcGreeterWorker.Consumers
{
    public class HelloRequestConsumer : IConsumer<HelloRequest>
    {
        private readonly ILogger<HelloRequestConsumer> logger;

        public HelloRequestConsumer(ILogger<HelloRequestConsumer> logger)
        {
            this.logger = logger;
        }
        
        public Task Consume(ConsumeContext<HelloRequest> context)
        {
            
            logger.LogInformation("Received Message: Hello {Name}", context.Message.Name);
            return Task.CompletedTask;
        }
    }
}