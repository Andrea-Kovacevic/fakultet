using Core.Models;

namespace Core.View;

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
                             .Distinct()
                             .ToList();
        k.IzdavacSifra = ConsoleInput.ReadOptional("Sifra izdavaca");
        return k;
    }

    private void Add()
    {
        string isbn = ConsoleInput.ReadString("ISBN");
        if (_controller.GetById(isbn) != null)
        {
            Console.WriteLine("Knjiga sa tim ISBN-om vec postoji.");
            return;
        }
        Console.WriteLine(_controller.Add(ReadKnjiga(isbn), out string err) ? "Knjiga dodata." : err);
    }

    private void Edit()
    {
        string isbn = ConsoleInput.ReadString("ISBN knjige za izmenu");
        if (_controller.GetById(isbn) == null) { Console.WriteLine("Knjiga ne postoji."); return; }
        Console.WriteLine(_controller.Edit(isbn, ReadKnjiga(isbn), out string err) ? "Izmenjeno." : err);
    }
}