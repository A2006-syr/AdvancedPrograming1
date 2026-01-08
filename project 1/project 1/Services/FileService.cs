using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace project_1.Services;

public static class FileService
{
    public static T Load<T>(string path) where T : new()
    {
        if (!File.Exists(path)) return new T();
        return JsonSerializer.Deserialize<T>(File.ReadAllText(path))!;
    }

    public static void Save<T>(string path, T data)
    {
        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(path, json);
    }
}

