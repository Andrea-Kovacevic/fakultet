using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models;

public class Adresa
{
    public string Ulica { get; set; } = "";
    public string Broj { get; set; } = "";
    public string Grad { get; set; } = "";
    public string Drzava { get; set; } = "";

    public override string ToString() => $"{Ulica} {Broj}, {Grad}, {Drzava}";
}
