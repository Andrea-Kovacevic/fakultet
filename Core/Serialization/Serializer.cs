using System.Text;

namespace Core.Serialization;

public class Serializer<T> where T : ISerializable, new()
{
    private const char Delimiter = '|';

    public string ToCSV(List<T> objects)
    {
        StringBuilder sb = new();
        foreach (T obj in objects)
            sb.AppendLine(string.Join(Delimiter, obj.ToCSV()));
        return sb.ToString();
    }

    public List<T> FromCSV(IEnumerable<string> lines)
    {
        List<T> objects = new();
        foreach (string line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) continue;
            T obj = new();
            obj.FromCSV(line.Split(Delimiter));
            objects.Add(obj);
        }
        return objects;
    }
}