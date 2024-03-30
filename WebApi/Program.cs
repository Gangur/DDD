using Microsoft.EntityFrameworkCore;
using Persistence;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AssemblyReference.Assembly));

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(
        options => options.UseSqlServer("Server=.; Database=DDD_DB; Integrated Security=true; MultipleActiveResultSets=true; TrustServerCertificate=True;"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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
