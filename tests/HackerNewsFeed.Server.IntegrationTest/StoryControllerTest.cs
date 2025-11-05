using DataContract;
using NewsService;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace HackerNewsFeed.Server.IntegrationTest;

[Collection("TestServer")]
public class StoryControllerTest(
    TestServer testServer
) : IAsyncLifetime
{
    private readonly TestServer _server = testServer;
    protected IServiceProvider? _serviceProvider;
    private AsyncServiceScope _scope;
    private HttpClient? _httpClient;

    public Task InitializeAsync()
    {
        _httpClient = _server.CreateClient(new WebApplicationFactoryClientOptions
        {
            BaseAddress = new Uri("https://localhost:5015")
        });
        _scope = _server.Services.CreateAsyncScope();
        _serviceProvider = _scope.ServiceProvider;
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _scope.DisposeAsync();
    }

    [Fact]
    public async Task GetStories_ReturnsOk()
    {
        // Arrange
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/story?pageIndex=0&pageSize=10");
        // Act
        var response = await _httpClient!.SendAsync(request);
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task SearchStories_ReturnsOk()
    {
        // Arrange
        var indexer = _serviceProvider!.GetRequiredService<IIndexer>();
        var deserializer = _serviceProvider!.GetRequiredService<ItemDeserializer>();
        var request = new HttpRequestMessage(HttpMethod.Get, "/api/story/search?searchTerm=498");
        // Act
        var response = await _httpClient!.SendAsync(request);
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        var storiesContent = deserializer.Deserialize<StoryControllerResponse>(content);

        Assert.NotNull(storiesContent);
        Assert.Single(storiesContent.Stories);
        Assert.Equal(1, storiesContent.Total);

        var item = storiesContent.Stories.First();
        Assert.Equal(498, item.Id);
        Assert.Equal("Mock Story 498", item.Title);
    }




}
