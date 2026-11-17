using Core.Models;
using Core.Storage;
using SajamKnjiga.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Controller;

public class KupovinaController
{
    private readonly DataContext _ctx;
    public KupovinaController(DataContext ctx) => _ctx = ctx;

    public List<Kupovina> GetAll() => _ctx.Kupovine;

    public Kupovina? GetById(string posetilacId, string isbn) =>
        _ctx.Kupovine.FirstOrDefault(k => k.PosetilacKartica == posetilacId && k.Isbn == isbn);

    public bool Add(Kupovina nova, out string error)
    {
        var posetilac = _ctx.Posetioci.FirstOrDefault(p => p.BrojClanskeKarte == nova.PosetilacKartica);
        if (posetilac == null) { error = "Posetilac ne postoji."; return false; }
        if (_ctx.Knjige.All(k => k.Isbn != nova.Isbn)) { error = "Knjiga ne postoji."; return false; }
        if (nova.Ocena < 1 || nova.Ocena > 5) { error = "Ocena mora biti od 1 do 5."; return false; }
        if (GetById(nova.PosetilacKartica, nova.Isbn) != null)
        {
            error = "Posetilac je vec kupio tu knjigu.";
            return false;
        }

        posetilac.ListaZeljaIsbn.Remove(nova.Isbn);   // kupljena knjiga ide sa liste zelja
        _ctx.Kupovine.Add(nova);
        _ctx.Commit();
        error = "";
        return true;
    }

    public bool Edit(string posetilacId, string isbn, int ocena, DateTime datum, string komentar,
                     out string error)
    {
        var k = GetById(posetilacId, isbn);
        if (k == null) { error = "Kupovina ne postoji."; return false; }
        if (ocena < 1 || ocena > 5) { error = "Ocena mora biti od 1 do 5."; return false; }

        k.Ocena = ocena;
        k.DatumKupovine = datum;
        k.Komentar = komentar;
        _ctx.Commit();
        error = "";
        return true;
    }

    public bool Delete(string posetilacId, string isbn)
    {
        var k = GetById(posetilacId, isbn);
        if (k == null) return false;
        _ctx.Kupovine.Remove(k);
        _ctx.Commit();
        return true;
    }
}

