using System.Threading;
using System.Threading.Tasks;
using GrpcGreeter;
using Microsoft.Extensions.Hosting;

namespace GrpcGreeterWorker.Workers
{
    public class Worker : BackgroundService
    {
        private readonly Greeter.GreeterClient client;

        public Worker(Greeter.GreeterClient client)
        {
            this.client = client;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await client.SayHelloAsync(new HelloRequest { Name = "Rich" });
                await Task.Delay(3000, stoppingToken);
            }
        }
    }
}