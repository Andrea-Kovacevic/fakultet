using Core.Serialization;

namespace Core.Storage;

public class CsvStorage<T> where T : ISerializable, new()
{
    private readonly string _filePath;
    private readonly Serializer<T> _serializer = new();

    public CsvStorage(string fileName)
    {
        string dir = AppContext.BaseDirectory + "Data";
        Directory.CreateDirectory(dir);
        _filePath = dir + Path.DirectorySeparatorChar + fileName;
    }

    public List<T> Load()
    {
        if (!File.Exists(_filePath)) return new List<T>();
        return _serializer.FromCSV(File.ReadLines(_filePath));
    }

    public void Save(List<T> objects)
    {
        File.WriteAllText(_filePath, _serializer.ToCSV(objects));
    }
}
