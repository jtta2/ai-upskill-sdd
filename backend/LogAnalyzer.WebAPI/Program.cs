using LogAnalyzer.Core.Interfaces;
using LogAnalyzer.Infrastructure.Data;
using LogAnalyzer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<LogDbContext>(options =>
	options.UseInMemoryDatabase("LogAnalyzerDb"));

builder.Services.AddScoped<ILogRepository, LogRepository>();

builder.Services.AddHttpClient();
builder.Services.AddScoped<IAIService>(serviceProvider =>
{
	var httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
	var httpClient = httpClientFactory.CreateClient();
	var apiKey = builder.Configuration["AI:ApiKey"] ?? "placeholder_key";
	var endpointUrl = builder.Configuration["AI:Endpoint"] ?? "https://placeholder_url";

	return new AzureAIService(httpClient, apiKey, endpointUrl);
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
		policy.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
});

var app = builder.Build();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();