using Core.Controller;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Models;

namespace Core.View;

public class KupovinaView
{
    private readonly KupovinaController _controller;
    public KupovinaView(KupovinaController c) => _controller = c;

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n--- KUPOVINE ---");
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
        var k = new Kupovina
        {
            PosetilacKartica = ConsoleInput.ReadString("Broj clanske karte posetioca"),
            Isbn = ConsoleInput.ReadString("ISBN knjige"),
            DatumKupovine = ConsoleInput.ReadDate("Datum kupovine"),
            Ocena = ConsoleInput.ReadInt("Ocena", 1, 5),
            Komentar = ConsoleInput.ReadOptional("Komentar") ?? ""
        };
        Console.WriteLine(_controller.Add(k, out string err) ? "Kupovina upisana." : err);
    }

    private void Edit()
    {
        string pos = ConsoleInput.ReadString("Broj clanske karte posetioca");
        string isbn = ConsoleInput.ReadString("ISBN knjige");
        if (_controller.GetById(pos, isbn) == null) { Console.WriteLine("Kupovina ne postoji."); return; }

        int ocena = ConsoleInput.ReadInt("Nova ocena", 1, 5);
        DateTime datum = ConsoleInput.ReadDate("Novi datum kupovine");
        string komentar = ConsoleInput.ReadOptional("Novi komentar") ?? "";
        Console.WriteLine(_controller.Edit(pos, isbn, ocena, datum, komentar, out string err)
            ? "Izmenjeno." : err);
    }

    private void Delete()
    {
        string pos = ConsoleInput.ReadString("Broj clanske karte posetioca");
        string isbn = ConsoleInput.ReadString("ISBN knjige");
        if (_controller.GetById(pos, isbn) == null) { Console.WriteLine("Kupovina ne postoji."); return; }
        if (!ConsoleInput.Confirm("Da li ste sigurni")) return;
        Console.WriteLine(_controller.Delete(pos, isbn) ? "Obrisano." : "Greska.");
    }

    private void ShowAll()
    {
        Console.WriteLine($"\n{"Posetilac",-14}{"ISBN",-16}{"Naziv knjige",-26}{"Datum",-12}{"Ocena",-7}Komentar");
        Console.WriteLine(new string('-', 95));
        foreach (var k in _controller.GetAll())
            Console.WriteLine($"{k.Id,-14}{k.Isbn,-16}{k.Knjiga?.Naziv ?? "?",-26}" +
                              $"{k.DatumKupovine:dd.MM.yyyy,-12}{k.Ocena,-7}{k.Komentar}");
    }
}
