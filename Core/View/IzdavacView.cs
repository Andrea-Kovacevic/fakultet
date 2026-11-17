using Core.Controller;
using Core.Models;

namespace Core.View;

public class IzdavacView
{
    private readonly IzdavacController _controller;
    public IzdavacView(IzdavacController c) => _controller = c;

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n--- IZDAVACI ---");
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

    private void Add()
    {
        string sifra = ConsoleInput.ReadString("Sifra");
        if (_controller.GetById(sifra) != null)
        {
            Console.WriteLine("Izdavac sa tom sifrom vec postoji.");
            return;
        }
        var i = new Izdavac
        {
            Sifra = sifra,
            Naziv = ConsoleInput.ReadString("Naziv"),
            SefLk = ConsoleInput.ReadOptional("Broj LK sefa (autor sa min. 5 god. iskustva)")
        };
        Console.WriteLine(_controller.Add(i, out string err) ? "Izdavac dodat." : err);
    }

    private void Edit()
    {
        string sifra = ConsoleInput.ReadString("Sifra izdavaca za izmenu");
        var postojeci = _controller.GetById(sifra);
        if (postojeci == null) { Console.WriteLine("Izdavac ne postoji."); return; }

        string naziv = ConsoleInput.ReadString("Novi naziv");
        string? sef = ConsoleInput.ReadOptional("Broj LK novog sefa (Enter = bez sefa)");
        Console.WriteLine(_controller.Edit(sifra, naziv, sef, out string err) ? "Izmenjeno." : err);
    }

    private void Delete()
    {
        string sifra = ConsoleInput.ReadString("Sifra izdavaca za brisanje");
        if (_controller.GetById(sifra) == null) { Console.WriteLine("Izdavac ne postoji."); return; }
        if (!ConsoleInput.Confirm("Da li ste sigurni")) return;
        Console.WriteLine(_controller.Delete(sifra) ? "Obrisano." : "Greska.");
    }

    private void ShowAll()
    {
        Console.WriteLine($"\n{"Sifra",-10}{"Naziv",-22}{"Sef",-24}{"Knjiga",-8}Autora");
        Console.WriteLine(new string('-', 75));
        foreach (var i in _controller.GetAll())
            Console.WriteLine($"{i.Sifra,-10}{i.Naziv,-22}" +
                              $"{(i.Sef == null ? "-" : i.Sef.Ime + " " + i.Sef.Prezime),-24}" +
                              $"{i.Knjige.Count,-8}{i.Autori.Count}");
    }

    private void ShowDetails()
    {
        string sifra = ConsoleInput.ReadString("Sifra izdavaca");
        var i = _controller.GetById(sifra);
        if (i == null) { Console.WriteLine("Izdavac ne postoji."); return; }

        Console.WriteLine($"\nSifra: {i.Sifra}");
        Console.WriteLine($"Naziv: {i.Naziv}");
        Console.WriteLine($"Sef: {(i.Sef == null ? "-" : i.Sef.Ime + " " + i.Sef.Prezime)}");
        Console.WriteLine("Knjige: " +
            (i.Knjige.Count == 0 ? "-" : string.Join(", ", i.Knjige.Select(k => k.Naziv))));
        Console.WriteLine("Autori: " +
            (i.Autori.Count == 0 ? "-" : string.Join(", ", i.Autori.Select(a => $"{a.Ime} {a.Prezime}"))));
    }
}