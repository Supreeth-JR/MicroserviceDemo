using MassTransit;
using Products.Service.Models.Dtos;
using System.Text.Json;

namespace Products.Services;

[ExcludeFromConfigureEndpoints]
public class OrderConsumer : IConsumer
{
    public async Task Consume(ConsumeContext context)
    {
        var msg = context;

        await Console.Out.WriteLineAsync(JsonSerializer.Serialize(msg));
    }
}
