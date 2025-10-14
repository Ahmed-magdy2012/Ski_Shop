using Microsoft.EntityFrameworkCore;
using SKINET.Server.Entities.Interfaces;
using SKINET.Server.Infrastracture.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddControllers();
builder.Services.AddDbContext<StoreContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("Default")
    ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
