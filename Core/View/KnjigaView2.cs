using System;
using System.Collections.Generic;
using System.Text;
using Core.Controller;
using Core.Models;

namespace Core.View;

// Student 1: dodavanje i izmena knjige.
// Polje _controller, Menu, ShowAll i Delete su u KnjigaView.Student2.cs (partial class).
public partial class KnjigaView
{
    private Knjiga ReadKnjiga(string isbn)
    {
        var k = new Knjiga
        {
            Isbn = isbn,
            Naziv = ConsoleInput.ReadString("Naziv"),
            Zanr = ConsoleInput.ReadString("Zanr"),
            GodinaIzdanja = ConsoleInput.ReadInt("Godina izdanja", 1400, 2100),
            Cena = ConsoleInput.ReadDecimal("Cena"),
            BrojStrana = ConsoleInput.ReadInt("Broj strana", 1, 100000)
        };

        string lkLista = ConsoleInput.ReadString("Brojevi LK autora (odvojeni zarezom)");
        k.AutoriIds = lkLista.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                             .ToList();
        k.IzdavacSifra = ConsoleInput.ReadOptional("Sifra izdavaca");
        return k;
    }

    private void Add()
    {
        string isbn = ConsoleInput.ReadString("ISBN");
        Console.WriteLine(_controller.Add(ReadKnjiga(isbn), out string err) ? "Knjiga dodata." : err);
    }

    private void Edit()
    {
        string isbn = ConsoleInput.ReadString("ISBN knjige za izmenu");
        if (_controller.GetById(isbn) == null) { Console.WriteLine("Knjiga ne postoji."); return; }
        Console.WriteLine(_controller.Edit(isbn, ReadKnjiga(isbn), out string err) ? "Izmenjeno." : err);
    }
}
