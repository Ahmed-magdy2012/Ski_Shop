using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SKINET.Server.Entities;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Infrastracture.Data;
using SKINET.Server.Infrastracture.NewFolder;
using SKINET.Server.Middlewares;
using StackExchange.Redis;


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
builder.Services.AddIdentityApiEndpoints<AppUser>().AddEntityFrameworkStores<StoreContext>();

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepo<>));
builder.Services.AddControllers();


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

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<AppUser>();

var scope =app.Services.CreateScope().ServiceProvider.GetRequiredService<StoreContext>();
await scope.Database.MigrateAsync();
await SeedData.seeding(scope);



app.Run();
