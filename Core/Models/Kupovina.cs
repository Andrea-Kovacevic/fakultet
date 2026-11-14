using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Kupovina
{
    public int Id { get; set; }
    public string PosetilacKartica { get; set; } = "";
    public string Isbn { get; set; } = "";
    public DateTime DatumKupovine { get; set; }
    public int Ocena { get; set; }             
    public string Komentar { get; set; } = "";
    [JsonIgnore] public Posetilac? Posetilac { get; set; }
    [JsonIgnore] public Knjiga? Knjiga { get; set; }
}

