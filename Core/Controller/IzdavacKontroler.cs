using Core.Models;
using Core.Storage;

namespace Core.Controller;

public class IzdavacController
{
    private readonly DataContext _ctx;
    public IzdavacController(DataContext ctx) => _ctx = ctx;

    public List<Izdavac> GetAll() => _ctx.Izdavaci;

    public Izdavac? GetById(string sifra) =>
        _ctx.Izdavaci.FirstOrDefault(i => i.Sifra == sifra);

    /// <summary>Sef mora postojati i imati najmanje 5 godina iskustva.</summary>
    private bool ValidateSef(string? sefId, out string error)
    {
        error = "";
        if (sefId == null) return true;
        var autor = _ctx.Autori.FirstOrDefault(a => a.BrojLicneKarte == sefId);
        if (autor == null) { error = "Autor (sef) ne postoji."; return false; }
        if (autor.GodineIskustva < 5)
        {
            error = "Sef izdavaca mora imati najmanje 5 godina iskustva.";
            return false;
        }
        return true;
    }

    public bool Add(Izdavac novi, out string error)
    {
        if (GetById(novi.Sifra) != null) { error = "Izdavac sa tom sifrom vec postoji."; return false; }
        if (!ValidateSef(novi.SefLk, out error)) return false;

        _ctx.Izdavaci.Add(novi);
        _ctx.Commit();
        return true;
    }

    public bool Edit(string sifra, string noviNaziv, string? noviSefId, out string error)
    {
        var i = GetById(sifra);
        if (i == null) { error = "Izdavac ne postoji."; return false; }
        if (!ValidateSef(noviSefId, out error)) return false;

        i.Naziv = noviNaziv;
        i.SefLk = noviSefId;
        _ctx.Commit();
        return true;
    }

    public bool Delete(string sifra)
    {
        var i = GetById(sifra);
        if (i == null) return false;

        foreach (var k in _ctx.Knjige.Where(k => k.IzdavacSifra == sifra))
            k.IzdavacSifra = null;                          // knjige ostaju bez izdavaca

        _ctx.Izdavaci.Remove(i);
        _ctx.Commit();
        return true;
    }
}
