using MediatR;
using Microsoft.EntityFrameworkCore;
using UserIdentity.Infrastructure;
using UserIdentity.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add controllers
builder.Services.AddControllers();

// Add infrastructure services
builder.Services.AddInfrastructureServices();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddMediatR(cfg =>
// {
    
//     cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);

// });

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowAll");

// Use routing and endpoints
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
