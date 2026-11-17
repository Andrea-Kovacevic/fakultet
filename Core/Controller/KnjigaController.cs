using Core.Models;
using Core.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Controller;

public partial class KnjigaController
{
private readonly DataContext _ctx;
public KnjigaController(DataContext ctx) => _ctx = ctx;

public List<Knjiga> GetAll() => _ctx.Knjige;

public Knjiga? GetById(string isbn) =>
    _ctx.Knjige.FirstOrDefault(k => k.Isbn == isbn);

public bool Delete(string isbn)
{
    var k = GetById(isbn);
    if (k == null) return false;

    _ctx.Kupovine.RemoveAll(x => x.Isbn == isbn);       
    foreach (var p in _ctx.Posetioci)
        p.ListaZeljaIsbn.Remove(isbn);                        
 
    _ctx.Knjige.Remove(k);
    _ctx.Commit();
    return true;
}
}