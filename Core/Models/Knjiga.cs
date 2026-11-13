using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Knjiga
{
    public string Isbn { get; set; } = "";             
    public string Naziv { get; set; } = "";
    public string Zanr { get; set; } = "";
    public int GodinaIzdanja { get; set; }
    public decimal Cena { get; set; }
    public int BrojStrana { get; set; }
    public List<string> AutoriIds { get; set; } = new();   
    public string? IzdavacSifra { get; set; }

    [JsonIgnore] public List<Autor> Autori { get; set; } = new();
    [JsonIgnore] public Izdavac? Izdavac { get; set; }
    [JsonIgnore] public List<Posetilac> Kupci { get; set; } = new();
    [JsonIgnore] public List<Posetilac> ZeljniPosetioci { get; set; } = new();
}
