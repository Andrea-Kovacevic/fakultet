using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Autor
{
    public string BrojLicneKarte { get; set; } = "";   
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public DateTime DatumRodjenja { get; set; }
    public Adresa Adresa { get; set; } = new();
    public string Telefon { get; set; } = "";
    public string Email { get; set; } = "";
    public int GodineIskustva { get; set; }

    [JsonIgnore] public List<Knjiga> Knjige { get; set; } = new();
}
