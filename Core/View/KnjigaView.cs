using System;
using System.Collections.Generic;
using System.Text;
using Core.Controller;


namespace Core.View;

public partial class KnjigaView
{
    private readonly KnjigaController _controller;
    public KnjigaView(KnjigaController c) => _controller = c;

    public void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n--- KNJIGE ---");
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

    private void Delete()
    {
        string isbn = ConsoleInput.ReadString("ISBN knjige za brisanje");
        if (_controller.GetById(isbn) == null) { Console.WriteLine("Knjiga ne postoji."); return; }
        if (!ConsoleInput.Confirm("Da li ste sigurni")) return;
        Console.WriteLine(_controller.Delete(isbn) ? "Obrisano." : "Greska.");
    }

    private void ShowAll()
    {
        Console.WriteLine($"\n{"ISBN",-16}{"Naziv",-26}{"Zanr",-14}{"God.",-6}{"Cena",-10}{"Str.",-6}{"Izdavac",-16}Autori");
        Console.WriteLine(new string('-', 120));
        foreach (var k in _controller.GetAll())
        {
            string autori = string.Join(", ", k.Autori.Select(a => $"{a.Ime} {a.Prezime}"));
            Console.WriteLine($"{k.Isbn,-16}{k.Naziv,-26}{k.Zanr,-14}{k.GodinaIzdanja,-6}" +
                              $"{k.Cena,-10:0.00}{k.BrojStrana,-6}{k.Izdavac?.Naziv ?? "-",-16}{autori}");
        }
    }
}
