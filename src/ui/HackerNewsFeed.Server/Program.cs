using HackerNewsFeed.Server.Plumbing;
using Scalar.AspNetCore;

public class Program
{
    public static void Main(string[] args)
    {
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

        builder.Services.AddStoryServices(builder.Configuration);

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

        //app.MapFallbackToFile("/index.html");

        app.Run();
    }
}