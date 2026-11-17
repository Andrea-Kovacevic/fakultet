using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Controller;
    public class AutorController
    {
    private readonly DataContext _ctx;
    public AutorController(DataContext ctx) => _ctx = ctx;

    public List<Autor> GetAll() => _ctx.Autori;

    public Autor? GetById(string brojLk) =>
        _ctx.Autori.FirstOrDefault(a => a.BrojLicneKarte == brojLk);

    public bool Add(Autor novi, out string error)
    {
        if (GetById(novi.BrojLicneKarte) != null)
        {
            error = "Autor sa tim brojem licne karte vec postoji.";
            return false;
        }
        if (novi.GodineIskustva < 0)
        {
            error = "Godine iskustva ne mogu biti negativne.";
            return false;
        }
        if (!novi.Email.Contains('@'))
        {
            error = "Email nije validan.";
            return false;
        }
        _ctx.Autori.Add(novi);
        _ctx.Commit();
        error = "";
        return true;
    }

    public bool Edit(string brojLk, Autor izmenjen, out string error)
    {
        var a = GetById(brojLk);
        if (a == null) { error = "Autor ne postoji."; return false; }
        if (izmenjen.GodineIskustva < 0) { error = "Godine iskustva ne mogu biti negativne."; return false; }
        if (!izmenjen.Email.Contains('@')) { error = "Email nije validan."; return false; }

        if (izmenjen.GodineIskustva < 5 && _ctx.Izdavaci.Any(i => i.SefLk == brojLk))
        {
            error = "Autor je sef izdavaca i mora imati bar 5 godina iskustva.";
            return false;
        }

        a.Ime = izmenjen.Ime;
        a.Prezime = izmenjen.Prezime;
        a.DatumRodjenja = izmenjen.DatumRodjenja;
        a.Adresa = izmenjen.Adresa;
        a.Telefon = izmenjen.Telefon;
        a.Email = izmenjen.Email;
        a.GodineIskustva = izmenjen.GodineIskustva;
        _ctx.Commit();
        error = "";
        return true;
    }

    public bool Delete(string brojLk, out string message)
    {
        var a = GetById(brojLk);
        if (a == null) { message = "Autor ne postoji."; return false; }
        

        if (_ctx.Knjige.Any(k => k.AutoriIds.Count == 1 && k.AutoriIds[0] == brojLk))
        {
            message = "Autor je jedini autor neke knjige. Prvo obrisite ili izmenite tu knjigu.";
            return false;
        }

        message = "";
        var izdavaciSef = _ctx.Izdavaci.Where(i => i.SefLk == brojLk).ToList();
        if (izdavaciSef.Count > 0)
            message = "Napomena: autor je bio sef izdavaca (" +
                      string.Join(", ", izdavaciSef.Select(i => i.Naziv)) + "), sef je uklonjen.";

        foreach (var k in _ctx.Knjige)
            k.AutoriIds.Remove(brojLk);                       
        foreach (var i in _ctx.Izdavaci.Where(i => i.SefLk == brojLk))
            i.SefLk = null;                                   

        _ctx.Autori.Remove(a);
        _ctx.Commit();
        return true;
    }
}
