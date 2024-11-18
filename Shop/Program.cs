using BusinesLayer.Features;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Restoran.Entity;
using Restoran.Service;
using Restoran.Service.Interface;
using Shop.Service.Interface;
using Shop.Servises;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Ðåºñòðàö³ÿ MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetMenuBuIdHandler).Assembly));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISubMenuService, SubMenuService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IDishService, DishService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();

// Register validators
builder.Services.AddTransient<IValidator<Menu>, MenuValidator>();
builder.Services.AddTransient<IValidator<SubMenu>, SubMenuValidator>();
builder.Services.AddTransient<IValidator<Dish>, DishValidator>();
builder.Services.AddTransient<IValidator<Order>, OrderValidator>();
builder.Services.AddTransient<IValidator<Receipt>, ReceiptValidator>();
builder.Services.AddTransient<IValidator<SimpleDish>, SimpleDishValidator>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Add EF Core DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
