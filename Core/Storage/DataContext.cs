using Core.Models;
using Core.Storage;
using Core.Serialization;

namespace Core.Storage;

public class DataContext
{
    public List<Posetilac> Posetioci { get; private set; } = new();
    public List<Autor> Autori { get; private set; } = new();
    public List<Knjiga> Knjige { get; private set; } = new();
    public List<Kupovina> Kupovine { get; private set; } = new();
    public List<Izdavac> Izdavaci { get; private set; } = new();

    private readonly CsvStorage<Posetilac> _posetiociFile = new("posetioci.csv");
    private readonly CsvStorage<Autor> _autoriFile = new("autori.csv");
    private readonly CsvStorage<Knjiga> _knjigeFile = new("knjige.csv");
    private readonly CsvStorage<Kupovina> _kupovineFile = new("kupovine.csv");
    private readonly CsvStorage<Izdavac> _izdavaciFile = new("izdavaci.csv");
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
