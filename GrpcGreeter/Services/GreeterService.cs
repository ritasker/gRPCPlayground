using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GrpcGreeter.Services;

public class GreeterService : Greeter.GreeterBase
{
    private readonly ILogger<GreeterService> logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        this.logger = logger;
    }

    public override Task<Empty> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new Empty());
    }
}