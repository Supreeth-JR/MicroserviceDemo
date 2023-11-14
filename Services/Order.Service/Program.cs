using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Service.Models;
using Orders.Service.Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(busConfig => 
{
    busConfig.SetKebabCaseEndpointNameFormatter(); // order-update-event

    busConfig.UsingRabbitMq((context, config) => 
    {
        // config.Host("amqp://guest:guest@localhost:5672");
        config.Host(new Uri(builder.Configuration["ServiceBus:Host"]), host =>
        {
            host.Username(builder.Configuration["ServiceBus:Username"]);
            host.Username(builder.Configuration["ServiceBus:Password"]);
        });

        // This will take above configuration and convert it to requried topology on broker
        // and helps in creating queue, exchange and buind them to messages which are being sent.
        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransitHostedService();



// Add services to the container.
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddMediatR(conf =>
{
    conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddControllers();

builder.Services.AddDbContext<NorthwindContext>(conf =>
{
    conf.UseSqlServer(builder.Configuration.GetConnectionString("Orders"));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
