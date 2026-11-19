using System.Globalization;
using Core.Serialization;

namespace Core.Models;

public class Knjiga : ISerializable
{
    public string Isbn { get; set; } = "";
    public string Naziv { get; set; } = "";
    public string Zanr { get; set; } = "";
    public int GodinaIzdanja { get; set; }
    public decimal Cena { get; set; }
    public int BrojStrana { get; set; }
    public List<string> AutoriIds { get; set; } = new();
    public string? IzdavacSifra { get; set; }

    public List<Autor> Autori { get; set; } = new();
    public Izdavac? Izdavac { get; set; }
    public List<Posetilac> Kupci { get; set; } = new();
    public List<Posetilac> ZeljniPosetioci { get; set; } = new();


    public string[] ToCSV() => new[]
    {
        Isbn, Naziv, Zanr, GodinaIzdanja.ToString(),
        Cena.ToString(CultureInfo.InvariantCulture), BrojStrana.ToString(),
        string.Join(';', AutoriIds), IzdavacSifra ?? ""
    };

    public void FromCSV(string[] v)
    {
        Isbn = v[0];
        Naziv = v[1];
        Zanr = v[2];
        GodinaIzdanja = int.Parse(v[3]);
        Cena = decimal.Parse(v[4], CultureInfo.InvariantCulture);
        BrojStrana = int.Parse(v[5]);
        AutoriIds = v[6].Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
        IzdavacSifra = string.IsNullOrEmpty(v[7]) ? null : v[7];
    }
}