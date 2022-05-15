using System;
using System.Reflection;
using Grpc.Net.Client;
using GrpcGreeter;
using GrpcGreeterWorker.Consumers;
using GrpcGreeterWorker.Workers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpcClient<Greeter.GreeterClient>(o =>
{
    o.Address = new Uri("https://localhost:7246");
});
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<HelloRequestConsumer>();
builder.Services.AddMassTransit(x =>
{
    var entryAssembly = Assembly.GetEntryAssembly();
    x.AddConsumers(entryAssembly);
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        
        cfg.ConfigureEndpoints(ctx);
    });
});

var app = builder.Build();
app.Run();