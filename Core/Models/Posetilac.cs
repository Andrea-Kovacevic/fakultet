using System.Globalization;
using Core.Serialization;

namespace Core.Models;

public class Posetilac : ISerializable
{
    public string BrojClanskeKarte { get; set; } = "";
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public DateTime DatumRodjenja { get; set; }
    public Adresa Adresa { get; set; } = new();
    public string Telefon { get; set; } = "";
    public string Email { get; set; } = "";
    public int GodinaClanstva { get; set; }
    public StatusClanstva Status { get; set; }
    public List<string> ListaZeljaIsbn { get; set; } = new();

    public List<Kupovina> Kupovine { get; set; } = new();
    public List<Knjiga> ListaZelja { get; set; } = new();
    public double ProsecnaOcena =>
        Kupovine.Count == 0 ? 0 : Kupovine.Average(k => k.Ocena);

    public string[] ToCSV() => new[]
    {
        BrojClanskeKarte, Ime, Prezime, DatumRodjenja.ToString("yyyy-MM-dd"),
        Adresa.Ulica, Adresa.Broj, Adresa.Grad, Adresa.Drzava,
        Telefon, Email, GodinaClanstva.ToString(), Status.ToString(),
        string.Join(';', ListaZeljaIsbn)
    };

    public void FromCSV(string[] v)
    {
        BrojClanskeKarte = v[0];
        Ime = v[1];
        Prezime = v[2];
        DatumRodjenja = DateTime.ParseExact(v[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
        Adresa = new Adresa { Ulica = v[4], Broj = v[5], Grad = v[6], Drzava = v[7] };
        Telefon = v[8];
        Email = v[9];
        GodinaClanstva = int.Parse(v[10]);
        Status = Enum.Parse<StatusClanstva>(v[11]);
        ListaZeljaIsbn = v[12].Split(';', StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}