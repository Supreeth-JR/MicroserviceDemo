using Microsoft.EntityFrameworkCore;
using Orders.Service.Models;
using Orders.Service.Repository;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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
