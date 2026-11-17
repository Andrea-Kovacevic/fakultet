using Core.Models;
using Core.Storage;

namespace Core.Controller;

public class PosetilacController
{
    private readonly DataContext _ctx;
    public PosetilacController(DataContext ctx) => _ctx = ctx;

    public List<Posetilac> GetAll() => _ctx.Posetioci;

    public Posetilac? GetById(string broj) =>
        _ctx.Posetioci.FirstOrDefault(p => p.BrojClanskeKarte == broj);

    public bool Add(Posetilac novi, out string error)
    {
        if (GetById(novi.BrojClanskeKarte) != null)
        {
            error = "Posetilac sa tim brojem clanske karte vec postoji.";
            return false;
        }
        if (!novi.Email.Contains('@'))
        {
            error = "Email nije validan.";
            return false;
        }
        _ctx.Posetioci.Add(novi);
        _ctx.Commit();
        error = "";
        return true;
    }

    public bool Edit(string broj, Posetilac izmenjen, out string error)
    {
        var p = GetById(broj);
        if (p == null) { error = "Posetilac ne postoji."; return false; }
        if (!izmenjen.Email.Contains('@')) { error = "Email nije validan."; return false; }

        p.Ime = izmenjen.Ime;
        p.Prezime = izmenjen.Prezime;
        p.DatumRodjenja = izmenjen.DatumRodjenja;
        p.Adresa = izmenjen.Adresa;
        p.Telefon = izmenjen.Telefon;
        p.Email = izmenjen.Email;
        p.GodinaClanstva = izmenjen.GodinaClanstva;
        p.Status = izmenjen.Status;
        // broj clanske karte i liste (kupovine, zelje) se ovde ne menjaju
        _ctx.Commit();
        error = "";
        return true;
    }

    public bool Delete(string broj)
    {
        var p = GetById(broj);
        if (p == null) return false;
        _ctx.Kupovine.RemoveAll(k => k.PosetilacKartica == broj);   // referencijalna zavisnost
        _ctx.Posetioci.Remove(p);
        _ctx.Commit();
        return true;
    }
}
