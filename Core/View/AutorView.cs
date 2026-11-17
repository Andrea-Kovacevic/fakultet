using Core.Controller;
using Core.Models;

namespace Core.View;

public class AutorView
{
    private readonly AutorController _controller;
    public AutorView(AutorController c) => _controller = c;

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n--- AUTORI ---");
            Console.WriteLine("1 Dodaj | 2 Izmeni | 3 Obrisi | 4 Prikazi | 5 Detalji | 0 Nazad");
            switch (ConsoleInput.ReadInt("Izbor", 0, 5))
            {
                case 1: Add(); break;
                case 2: Edit(); break;
                case 3: Delete(); break;
                case 4: ShowAll(); break;
                case 5: ShowDetails(); break;
                case 0: return;
            }
        }
    }

    private Autor ReadAutor(string brojLk) => new()
    {
        BrojLicneKarte = brojLk,
        Ime = ConsoleInput.ReadString("Ime"),
        Prezime = ConsoleInput.ReadString("Prezime"),
        DatumRodjenja = ConsoleInput.ReadDate("Datum rodjenja"),
        Telefon = ConsoleInput.ReadString("Telefon"),
        Email = ConsoleInput.ReadString("Email"),
        GodineIskustva = ConsoleInput.ReadInt("Godine iskustva", 0, 100),
        Adresa = new Adresa
        {
            Ulica = ConsoleInput.ReadString("Ulica"),
            Broj = ConsoleInput.ReadString("Broj"),
            Grad = ConsoleInput.ReadString("Grad"),
            Drzava = ConsoleInput.ReadString("Drzava")
        }
    };

    private void Add()
    {
        string lk = ConsoleInput.ReadString("Broj licne karte");
        if (_controller.GetById(lk) != null)
        {
            Console.WriteLine("Autor sa tim brojem licne karte vec postoji.");
            return;
        }
        Console.WriteLine(_controller.Add(ReadAutor(lk), out string err) ? "Autor dodat." : err);
    }

    private void Edit()
    {
        string lk = ConsoleInput.ReadString("Broj licne karte autora za izmenu");
        if (_controller.GetById(lk) == null) { Console.WriteLine("Autor ne postoji."); return; }
        Console.WriteLine(_controller.Edit(lk, ReadAutor(lk), out string err) ? "Izmenjeno." : err);
    }

    private void Delete()
    {
        string lk = ConsoleInput.ReadString("Broj licne karte autora za brisanje");
        if (_controller.GetById(lk) == null) { Console.WriteLine("Autor ne postoji."); return; }
        if (!ConsoleInput.Confirm("Da li ste sigurni")) return;

        bool ok = _controller.Delete(lk, out string msg);
        Console.WriteLine(ok ? "Obrisano." : msg);
        if (ok && msg.Length > 0) Console.WriteLine(msg);
    }

    private void ShowAll()
    {
        Console.WriteLine($"\n{"LK",-12}{"Ime",-12}{"Prezime",-14}{"Rodjen",-12}{"Iskustvo",-9}{"Email",-26}Adresa");
        Console.WriteLine(new string('-', 105));
        foreach (var a in _controller.GetAll())
            Console.WriteLine($"{a.BrojLicneKarte,-12}{a.Ime,-12}{a.Prezime,-14}" +
                              $"{a.DatumRodjenja,-12:dd.MM.yyyy}{a.GodineIskustva,-9}{a.Email,-26}{a.Adresa}");
    }

    private void ShowDetails()
    {
        string lk = ConsoleInput.ReadString("Broj licne karte autora");
        var a = _controller.GetById(lk);
        if (a == null) { Console.WriteLine("Autor ne postoji."); return; }

        Console.WriteLine($"\nLK: {a.BrojLicneKarte}");
        Console.WriteLine($"Ime i prezime: {a.Ime} {a.Prezime}");
        Console.WriteLine($"Datum rodjenja: {a.DatumRodjenja:dd.MM.yyyy}");
        Console.WriteLine($"Adresa: {a.Adresa}");
        Console.WriteLine($"Telefon: {a.Telefon}");
        Console.WriteLine($"Email: {a.Email}");
        Console.WriteLine($"Godine iskustva: {a.GodineIskustva}");
        Console.WriteLine("Knjige: " +
            (a.Knjige.Count == 0 ? "-" : string.Join(", ", a.Knjige.Select(k => k.Naziv))));
    }
}