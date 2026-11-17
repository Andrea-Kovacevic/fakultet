using Core.Models;
using Core.View;
using Core.Controller;

namespace Core.View;

public class PosetilacView
{
    private readonly PosetilacController _controller;
    public PosetilacView(PosetilacController c) => _controller = c;

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n--- POSETIOCI ---");
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

    private Posetilac ReadPosetilac(string broj) => new()
    {
        BrojClanskeKarte = broj,
        Ime = ConsoleInput.ReadString("Ime"),
        Prezime = ConsoleInput.ReadString("Prezime"),
        DatumRodjenja = ConsoleInput.ReadDate("Datum rodjenja"),
        Telefon = ConsoleInput.ReadString("Telefon"),
        Email = ConsoleInput.ReadString("Email"),
        GodinaClanstva = ConsoleInput.ReadInt("Godina clanstva", 1900, 2100),
        Status = ConsoleInput.ReadInt("Status (0 Redovan, 1 VIP)", 0, 1) == 0
            ? StatusClanstva.Redovan : StatusClanstva.VIP,
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
        string broj = ConsoleInput.ReadString("Broj clanske karte (npr. SK-1-2023)");
        Console.WriteLine(_controller.Add(ReadPosetilac(broj), out string err) ? "Posetilac dodat." : err);
    }

    private void Edit()
    {
        string broj = ConsoleInput.ReadString("Broj clanske karte posetioca za izmenu");
        if (_controller.GetById(broj) == null) { Console.WriteLine("Posetilac ne postoji."); return; }
        Console.WriteLine(_controller.Edit(broj, ReadPosetilac(broj), out string err) ? "Izmenjeno." : err);
    }

    private void Delete()
    {
        string broj = ConsoleInput.ReadString("Broj clanske karte posetioca za brisanje");
        if (_controller.GetById(broj) == null) { Console.WriteLine("Posetilac ne postoji."); return; }
        if (!ConsoleInput.Confirm("Da li ste sigurni")) return;
        Console.WriteLine(_controller.Delete(broj) ? "Obrisano." : "Greska.");
    }

    private void ShowAll()
    {
        Console.WriteLine($"\n{"Karta",-14}{"Ime",-12}{"Prezime",-14}{"Rodjen",-12}{"Status",-9}{"Prosek",-7}{"Zelje",-6}Adresa");
        Console.WriteLine(new string('-', 100));
        foreach (var p in _controller.GetAll())
            Console.WriteLine($"{p.BrojClanskeKarte,-14}{p.Ime,-12}{p.Prezime,-14}" +
                              $"{p.DatumRodjenja,-12:dd.MM.yyyy}{p.Status,-9}{p.ProsecnaOcena,-7:0.0}" +
                              $"{p.ListaZelja.Count,-6}{p.Adresa}");
    }
}
