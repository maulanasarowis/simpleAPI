using Microsoft.EntityFrameworkCore;
using simpleAPI.Data;
using simpleAPI.Repositories;
using simpleAPI.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add EF Core with PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Daftarkan semua layanan dan repository secara otomatis
RegisterServices(builder.Services);

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

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


void RegisterServices(IServiceCollection services)
{
    var assembly = Assembly.GetExecutingAssembly();

    // Daftarkan semua antarmuka dan implementasi yang sesuai
    var types = assembly.GetTypes();

    foreach (var type in types)
    {
        var interfaces = type.GetInterfaces();

        foreach (var iface in interfaces)
        {
            if (iface.Name.StartsWith("I") && iface.Name.Substring(1) == type.Name)
            {
                services.AddScoped(iface, type);
            }
        }
    }
}
