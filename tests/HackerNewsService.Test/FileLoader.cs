using NewsService.Test.Item;

namespace NewsService.Test;

internal static class FileLoader
{
    public static string LoadItemResponseJson(string fileName)
    {
        return LoadJson($"{typeof(DeserializeTest).Namespace}.{fileName}.json");
    }

    public static string LoadJson(string fileName)
    {
        var stream = typeof(FileLoader).Assembly.GetManifestResourceStream(fileName)
            ?? throw new FileNotFoundException(fileName);
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}
