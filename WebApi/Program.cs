using Application;
using Asp.Versioning.ApiExplorer;
using Infrastructure.DependencyInjections;
using Persistence;
using System.Text.Json.Serialization;
using WebApi;
using WebApi.Configurations;
using WebApi.Middleware;

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

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


builder.Services.AddVersioning();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddAuthenticationInfrastructure(configuration);

builder.Services.AddPersistence(configuration);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddCors();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

#if DEBUG
EnsureDatabaseInit.EnsureCreated(app);
#endif

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

var corsConf = configuration.GetRequiredSection("Cors");

ArgumentException.ThrowIfNullOrEmpty(corsConf["FrontendReact"]);
ArgumentException.ThrowIfNullOrEmpty(corsConf["FrontendAngular"]);

app.UseCors(opt =>
{
    var frontendReact = corsConf["FrontendReact"]!;
    var frontendAngular = corsConf["FrontendAngular"]!;

    opt.AllowAnyHeader()
       .AllowAnyMethod()
       .AllowCredentials()
       .WithOrigins(new[] { frontendReact, frontendAngular });
});

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
