using System.Globalization;
using Core.Serialization;

namespace Core.Models;

public class Kupovina : ISerializable
{
    public string PosetilacKartica { get; set; } = "";
    public string Isbn { get; set; } = "";
    public DateTime DatumKupovine { get; set; }
    public int Ocena { get; set; }
    public string Komentar { get; set; } = "";

    public Posetilac? Posetilac { get; set; }
    public Knjiga? Knjiga { get; set; }

    public string[] ToCSV() => new[]
    {
        PosetilacKartica, Isbn, DatumKupovine.ToString("yyyy-MM-dd"),
        Ocena.ToString(), Komentar.Replace('|', ' ')
    };

    public void FromCSV(string[] v)
    {
        PosetilacKartica = v[0];
        Isbn = v[1];
        DatumKupovine = DateTime.ParseExact(v[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Ocena = int.Parse(v[3]);
        Komentar = v[4];
    }
}