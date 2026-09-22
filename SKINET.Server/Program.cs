using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Infrastracture.Data;
using SKINET.Server.Infrastracture.NewFolder;
using SKINET.Server.Infrastracture.Services;
using SKINET.Server.Middlewares;
using SKINET.Server.NewFolder;
using StackExchange.Redis;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy
            .WithOrigins("https://localhost:4296")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
           
    });

});


// Add services to the container.

builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var config = builder.Configuration.GetConnectionString("Redis")
    ?? throw new Exception("cannot get redis connection string");
    var configration=ConfigurationOptions.Parse(config,true);
    return ConnectionMultiplexer.Connect(configration);
});

builder.Services.AddSingleton<ICartService, CartService>();

builder.Services.AddAuthentication();
builder.Services.AddIdentityApiEndpoints<AppUser>().AddRoles<IdentityRole>().
    AddEntityFrameworkStores<StoreContext>();

builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepo<>));
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IUnitOfWork, UnitodWork>();
builder.Services.AddSignalR();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("Default")
    ));



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    c => {
        c.AddServer(new OpenApiServer
        {
            Description = "Ski",
            Url = "https://localhost:7038"
        });

        c.CustomOperationIds(e =>
        e.ActionDescriptor.RouteValues.TryGetValue("action", out var action)
        ? action
        : e.ActionDescriptor.DisplayName
);
    });

var app = builder.Build();
app.UseMiddleware<middlewareException>();

app.UseCors("AllowAngular");
app.UseCors("CorsPolicy");

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGroup("api").MapIdentityApi<AppUser>();
app.MapHub<NotificationHub>("/hub/notifications");
var scope = app.Services.CreateScope();
var services =scope.ServiceProvider;
var context=services.GetRequiredService<StoreContext>();
var userManager = services.GetRequiredService<UserManager<AppUser>>();
await context.Database.MigrateAsync();
await  SeedData.seeding(context,userManager);



app.Run();
