using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace HackerNewsFeed.Server.IntegrationTest;

public class TestServer : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        var root = Directory.GetCurrentDirectory();
        
        var config = new ConfigurationBuilder()
                    .SetBasePath(root)
                    .AddJsonFile("TestSettings.json", optional: false, reloadOnChange: true)
                    .Build();
        builder.UseConfiguration(config);

        //builder.ConfigureAppConfiguration((context, config) => {
        //    config.Sources.Clear();
        //    var root = Directory.GetCurrentDirectory();
        //    var fileProvider = new PhysicalFileProvider(root);
        //    config.AddJsonFile(fileProvider, "TestSettings.json", optional: false, reloadOnChange: true);
        //});

    }
}
