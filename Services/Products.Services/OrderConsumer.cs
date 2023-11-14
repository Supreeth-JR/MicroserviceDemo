using Contrcts;
using MassTransit;
using MediatR;
using Products.Services.Commands;

namespace Products.Services;

public sealed class OrderConsumer : IConsumer<ProductQtyUpdateEvent>
{
    private readonly IMediator Mediator;

    public OrderConsumer(IMediator mediator)
    {
        Mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ProductQtyUpdateEvent> context)
    {
        await Mediator.Send(new UpdateProductQtyCommand(context.Message.Products));
    }
}
