using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Storage;

public class JsonStorage<T>
{
private static readonly JsonSerializerOptions Options = new()
{
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter() }
};

private readonly string _filePath;

public JsonStorage(string fileName)
{
    string dir = "Data";
    Directory.CreateDirectory(dir);
    _filePath = dir + Path.DirectorySeparatorChar + fileName;
}

public List<T> Load()
{
    if (!File.Exists(_filePath)) return new List<T>();
    string json = File.ReadAllText(_filePath);
    return JsonSerializer.Deserialize<List<T>>(json, Options) ?? new List<T>();
}

public void Save(List<T> items)
{
    File.WriteAllText(_filePath, JsonSerializer.Serialize(items, Options));
}
}
