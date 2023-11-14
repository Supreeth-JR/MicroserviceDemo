using MassTransit;
using Products.Services;
using Produts.Services.Repository;
using Serilog;
using Serilog.Events;
using System.Net;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(busConfig =>
{
    busConfig.SetKebabCaseEndpointNameFormatter(); // order-update-event
    busConfig.AddConsumer<OrderConsumer>();

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

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console()
    .CreateLogger();
builder.Services.AddSerilog((context, config) =>
{
    config.WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss}] [{Level:u3}] {Message:lj}{NewLine}{Exception}");
});

// Add services to the container.
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddScoped<IProductRepository, ProductRepository>(option =>
    new ProductRepository(builder.Configuration.GetConnectionString("Products")));


if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHttpsRedirection(options =>
    {
        options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
        options.HttpsPort = 443;
    });
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
