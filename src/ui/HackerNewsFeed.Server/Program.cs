using NewsService;
using Scalar.AspNetCore;
using ServiceContract;
using Caching;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEasyCaching(options =>
{
    options.UseInMemory("inmemory");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("https://localhost:63203")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.Configure<NewsOptions>(
    builder.Configuration.GetSection(NewsOptions.Position));
builder.Services.AddSingleton(ItemDeserializer.Instance);
builder.Services.AddSingleton<IStoryService, StoryServiceMock>();
builder.Services.TryDecorate<IStoryService, StoryServiceCache>();
//builder.Services.AddSingleton<IStoryService, StoryService>();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Demo Api");
    });
}

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
