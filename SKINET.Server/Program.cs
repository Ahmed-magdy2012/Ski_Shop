using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Infrastracture.Data;
using SKINET.Server.Middlewares;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepo<>));
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("Default")
    ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.AddServer(new OpenApiServer
    {
        Description = "SHOP",
        Url = "https://localhost:7038"
    });

    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] 
      }");
});

var app = builder.Build();
app.UseMiddleware<middlewareException>();
app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
var scope=app.Services.CreateScope().ServiceProvider.GetRequiredService<StoreContext>();
await scope.Database.MigrateAsync();
await SeedData.seeding(scope);



app.MapFallbackToFile("/index.html");

app.Run();
