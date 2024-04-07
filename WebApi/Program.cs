using Application;
using Asp.Versioning.ApiExplorer;
using Infrastructure;
using Persistence;
using Persistence.Data;
using WebApi;
using WebApi.Configurations;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;


//builder.Services.AddRebus(configure =>
//{
//    var configurer = configure
//        .Logging(l => l.ColoredConsole())
//        .Transport(t => t.UseRabbitMqAsOneWayClient("amqp://guest:guest@localhost:5672"));
//    return configurer;
//});

builder.Services.AddControllers();


builder.Services.AddVersioning();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddPersistence(configuration);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddCors();

var app = builder.Build();

#if DEBUG
EnsureDatabaseInit.EnsureCreated(app);
#endif

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader()
       .AllowAnyMethod()
       .WithOrigins(configuration.GetRequiredSection("Cors")["Frontend"] ?? 
            throw new ArgumentException("Cors configuration missing Frontend sources!"));
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
