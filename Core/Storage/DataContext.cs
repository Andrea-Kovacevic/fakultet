using Core.Models;
using Core.Storage;

namespace Core.Storage;

public class DataContext
{
    public List<Posetilac> Posetioci { get; private set; } = new();
    public List<Autor> Autori { get; private set; } = new();
    public List<Knjiga> Knjige { get; private set; } = new();
    public List<Kupovina> Kupovine { get; private set; } = new();
    public List<Izdavac> Izdavaci { get; private set; } = new();

    private readonly JsonStorage<Posetilac> _posetiociFile = new("posetioci.json");
    private readonly JsonStorage<Autor> _autoriFile = new("autori.json");
    private readonly JsonStorage<Knjiga> _knjigeFile = new("knjige.json");
    private readonly JsonStorage<Kupovina> _kupovineFile = new("kupovine.json");
    private readonly JsonStorage<Izdavac> _izdavaciFile = new("izdavaci.json");

    public void Load()
    {
        Posetioci = _posetiociFile.Load();
        Autori = _autoriFile.Load();
        Knjige = _knjigeFile.Load();
        Kupovine = _kupovineFile.Load();
        Izdavaci = _izdavaciFile.Load();
        Relink();
    }

    public void Save()
    {
        _posetiociFile.Save(Posetioci);
        _autoriFile.Save(Autori);
        _knjigeFile.Save(Knjige);
        _kupovineFile.Save(Kupovine);
        _izdavaciFile.Save(Izdavaci);
    }

    public void Commit()
    {
        Save();
        Relink();
    }

    private void Relink()
    {
        foreach (var p in Posetioci) { p.Kupovine.Clear(); p.ListaZelja.Clear(); }
        foreach (var a in Autori) a.Knjige.Clear();
        foreach (var i in Izdavaci) { i.Knjige.Clear(); i.Autori.Clear(); i.Sef = null; }
        foreach (var k in Knjige)
        {
            k.Autori.Clear(); k.Kupci.Clear(); k.ZeljniPosetioci.Clear(); k.Izdavac = null;
        }

        foreach (var k in Knjige)
        {
            foreach (var id in k.AutoriIds)
            {
                var autor = Autori.FirstOrDefault(a => a.BrojLicneKarte == id);
                if (autor == null) continue;
                k.Autori.Add(autor);
                autor.Knjige.Add(k);
            }

            k.Izdavac = Izdavaci.FirstOrDefault(i => i.Sifra == k.IzdavacSifra);
            if (k.Izdavac != null)
            {
                k.Izdavac.Knjige.Add(k);
                foreach (var a in k.Autori)
                    if (!k.Izdavac.Autori.Contains(a)) k.Izdavac.Autori.Add(a);
            }
        }

        foreach (var i in Izdavaci)
            i.Sef = Autori.FirstOrDefault(a => a.BrojLicneKarte == i.SefLk);

        foreach (var p in Posetioci)
            foreach (var isbn in p.ListaZeljaIsbn)
            {
                var k = Knjige.FirstOrDefault(x => x.Isbn == isbn);
                if (k == null) continue;
                p.ListaZelja.Add(k);
                k.ZeljniPosetioci.Add(p);
            }

        foreach (var kup in Kupovine)
        {
            kup.Posetilac = Posetioci.FirstOrDefault(p => p.BrojClanskeKarte == kup.PosetilacKartica);
            kup.Knjiga = Knjige.FirstOrDefault(k => k.Isbn == kup.Isbn);
            if (kup.Posetilac == null || kup.Knjiga == null) continue;
            kup.Posetilac.Kupovine.Add(kup);
            kup.Knjiga.Kupci.Add(kup.Posetilac);
        }
    }
}
