using Core.Models;
using Core.View;
using Core.Controller;

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
            Console.WriteLine("1 Dodaj | 2 Izmeni | 3 Obrisi | 4 Prikazi | 0 Nazad");
            switch (ConsoleInput.ReadInt("Izbor", 0, 4))
            {
                case 1: Add(); break;
                case 2: Edit(); break;
                case 3: Delete(); break;
                case 4: ShowAll(); break;
                case 0: return;
            }
        }
    }

    private void Add()
    {
        var i = new Izdavac
        {
            Sifra = ConsoleInput.ReadString("Sifra"),
            Naziv = ConsoleInput.ReadString("Naziv"),
            SefLk = ConsoleInput.ReadOptional("Broj LK sefa (autor sa min. 5 god. iskustva)")
        };
        Console.WriteLine(_controller.Add(i, out string err) ? "Izdavac dodat." : err);
    }

    private void Edit()
    {
        string sifra = ConsoleInput.ReadString("Sifra izdavaca za izmenu");
        if (_controller.GetById(sifra) == null) { Console.WriteLine("Izdavac ne postoji."); return; }

        string naziv = ConsoleInput.ReadString("Novi naziv");
        string? sef = ConsoleInput.ReadOptional("Broj LK novog sefa");
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
}
