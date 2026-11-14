using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Posetilac
{
    public string BrojClanskeKarte { get; set; } = ""; 
    public string Ime { get; set; } = "";
    public string Prezime { get; set; } = "";
    public DateTime DatumRodjenja { get; set; }
    public Adresa Adresa { get; set; } = new();
    public string Telefon { get; set; } = "";
    public string Email { get; set; } = "";
    public int GodinaClanstva { get; set; }
    public StatusClanstva Status { get; set; }
    public List<string> ListaZeljaIsbn { get; set; } = new();
    [JsonIgnore] public List<Kupovina> Kupovine { get; set; } = new();
    [JsonIgnore] public List<Knjiga> ListaZelja { get; set; } = new();
    [JsonIgnore]
    public double ProsecnaOcena =>
        Kupovine.Count == 0 ? 0 : Kupovine.Average(k => k.Ocena);
}