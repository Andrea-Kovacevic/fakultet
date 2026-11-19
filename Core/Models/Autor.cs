using System.Globalization;
using Core.Serialization;

namespace Core.Models;

public class Autor : ISerializable
{
    public string BrojLicneKarte { get; set; } = "";
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public DateTime DatumRodjenja { get; set; }
    public Adresa Adresa { get; set; } = new();
    public string Telefon { get; set; } = "";
    public string Email { get; set; } = "";
    public int GodineIskustva { get; set; }

    public List<Knjiga> Knjige { get; set; } = new();

    public string[] ToCSV() => new[]
    {
        BrojLicneKarte, Ime, Prezime, DatumRodjenja.ToString("yyyy-MM-dd"),
        Adresa.Ulica, Adresa.Broj, Adresa.Grad, Adresa.Drzava,
        Telefon, Email, GodineIskustva.ToString()
    };

    public void FromCSV(string[] v)
    {
        BrojLicneKarte = v[0];
        Ime = v[1];
        Prezime = v[2];
        DatumRodjenja = DateTime.ParseExact(v[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Adresa = new Adresa { Ulica = v[4], Broj = v[5], Grad = v[6], Drzava = v[7] };
        Telefon = v[8];
        Email = v[9];
        GodineIskustva = int.Parse(v[10]);
    }
}