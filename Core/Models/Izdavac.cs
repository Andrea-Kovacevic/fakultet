using System.Text.Json.Serialization;
using Core.Serialization;

namespace Core.Models;

public class Izdavac : ISerializable
{
    public string Sifra { get; set; } = "";
    public string Naziv { get; set; } = "";
    public string? SefLk { get; set; }

    [JsonIgnore] public Autor? Sef { get; set; }
    [JsonIgnore] public List<Knjiga> Knjige { get; set; } = new();
    [JsonIgnore] public List<Autor> Autori { get; set; } = new();

    public string[] ToCSV() => new[] { Sifra, Naziv, SefLk ?? "" };

    public void FromCSV(string[] v)
    {
        Sifra = v[0];
        Naziv = v[1];
        SefLk = string.IsNullOrEmpty(v[2]) ? null : v[2];
    }
}