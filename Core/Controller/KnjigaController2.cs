using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Controller;

// Student 1: dodavanje i izmena knjige.
// Konstruktor, GetAll, GetById i Delete su u KnjigaController.Student2.cs (partial class).
public partial class KnjigaController
{
    private bool Validate(Knjiga k, out string error)
    {
        error = "";
        if (k.Cena < 0) { error = "Cena ne moze biti negativna."; return false; }
        if (k.BrojStrana <= 0) { error = "Broj strana mora biti pozitivan."; return false; }
        if (k.AutoriIds.Count == 0) { error = "Knjiga mora imati bar jednog autora."; return false; }

        foreach (var id in k.AutoriIds)
            if (_ctx.Autori.All(a => a.BrojLicneKarte != id))
            {
                error = $"Autor sa LK {id} ne postoji.";
                return false;
            }

        if (k.IzdavacSifra != null && _ctx.Izdavaci.All(i => i.Sifra != k.IzdavacSifra))
        {
            error = "Izdavac sa tom sifrom ne postoji.";
            return false;
        }
        return true;
    }

    public bool Add(Knjiga nova, out string error)
    {
        if (GetById(nova.Isbn) != null) { error = "Knjiga sa tim ISBN-om vec postoji."; return false; }
        if (!Validate(nova, out error)) return false;

        _ctx.Knjige.Add(nova);
        _ctx.Commit();
        return true;
    }

    public bool Edit(string isbn, Knjiga izmenjena, out string error)
    {
        var k = GetById(isbn);
        if (k == null) { error = "Knjiga ne postoji."; return false; }
        if (!Validate(izmenjena, out error)) return false;

        k.Naziv = izmenjena.Naziv;
        k.Zanr = izmenjena.Zanr;
        k.GodinaIzdanja = izmenjena.GodinaIzdanja;
        k.Cena = izmenjena.Cena;
        k.BrojStrana = izmenjena.BrojStrana;
        k.AutoriIds = izmenjena.AutoriIds;
        k.IzdavacSifra = izmenjena.IzdavacSifra;
        // ISBN se ne menja (kljuc)
        _ctx.Commit();
        return true;
    }
}
