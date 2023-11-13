using MassTransit;
using Microsoft.EntityFrameworkCore;
using Orders.Service.Models;
using Orders.Service.Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(config => 
{
    config.UsingRabbitMq((ctx, cfg) => 
    {
        cfg.Host("amqp://guest:guest@localhost:5672");
    });
});

builder.Services.Configure<MassTransitHostOptions>(option =>
{
    option.WaitUntilStarted = true;
});


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
