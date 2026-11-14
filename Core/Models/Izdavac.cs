using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Izdavac
{
    public string Sifra { get; set; } = "";           
    public string Naziv { get; set; } = "";
    public string? SefLk { get; set; }               

    [JsonIgnore] public Autor? Sef { get; set; }
    [JsonIgnore] public List<Knjiga> Knjige { get; set; } = new();
    [JsonIgnore] public List<Autor> Autori { get; set; } = new();
}

