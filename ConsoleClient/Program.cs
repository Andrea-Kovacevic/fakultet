using Core.Controller;
using Core.View;
using Core.Storage;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;   

var ctx = new DataContext();
ctx.Load();                                // ucitavanje pri pokretanju

var posetiociView = new PosetilacView(new PosetilacController(ctx));
var autoriView = new AutorView(new AutorController(ctx));        // Student 2
var knjigeView = new KnjigaView(new KnjigaController(ctx));      // Student 1 + 2
var izdavaciView = new IzdavacView(new IzdavacController(ctx));
var kupovineView = new KupovinaView(new KupovinaController(ctx));

while (true)
{
    Console.WriteLine("\n=== SAJAM KNJIGA ===");
    Console.WriteLine("1 Posetioci | 2 Autori | 3 Knjige | 4 Izdavaci | 5 Kupovine | 0 Izlaz");
    switch (ConsoleInput.ReadInt("Izbor", 0, 5))
    {
        case 1: posetiociView.Menu(); break;
        case 2: autoriView.Menu(); break;
        case 3: knjigeView.Menu(); break;
        case 4: izdavaciView.Menu(); break;
        case 5: kupovineView.Menu(); break;
        case 0:
            ctx.Save();                    // cuvanje pri zatvaranju
            return;
    }
}
