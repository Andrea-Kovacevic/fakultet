# Sajam knjiga - informacioni sistem uprave sajma knjiga

Projektni zadatak iz predmeta **Osnovi informacionih sistema i softverskog inženjerstva** (2025/2026).

Aplikacija je namenjena referentu sajma knjiga. U ovoj fazi (prva kontrolna tačka) implementirani su model podataka, serijalizacija i funkcionalnosti 8.1-8.4 (dodavanje, izmena, brisanje i prikaz entiteta) kroz konzolnu aplikaciju.

## Tim

| Student | Broj indeksa |
|---|---|
| [Maša Sredić, prvi student] | [RA55/2024] |
| [Andrea Kovačević, drugi student] | [RA61/2024] |

## Podela posla

### Prvi student: [Maša Sredić]
- `#posetilac`: `Posetilac`, `StatusClanstva`, `PosetilacController`, `PosetilacView`
- `#kupovina`: `Kupovina`, `KupovinaController`, `KupovinaView`
- `#izdavac`: `Izdavac`, `IzdavacController`, `IzdavacView`
- `#dodavanje_posetioca`, `#izmena_posetioca`, `#brisanje_posetioca`, `#prikaz_posetilaca`
- `#dodavanje_knjige`, `#izmena_knjige` (`KnjigaController2.cs`, `KnjigaView2.cs`)

### Drugi student: [Andrea Kovačević]
- `#autor`: `Autor`, `AutorController`, `AutorView`
- `#knjiga`: `Knjiga`, `KnjigaController`, `KnjigaView`
- `#adresa`: `Adresa`
- `#dodavanje_autora`, `#izmena_autora`, `#brisanje_autora`, `#prikaz_autora`
- `#prikaz_knjiga`, `#brisanje_knjige`

### Zajedničko
- Skladištenje podataka: `DataContext`, `CSVStorage`
- Unos sa konzole: `ConsoleInput`
- Glavni meni: `Program.cs`

## Tehnologije
- C# / .NET 10
- Serijalizacija: CSV, fajlovi se čuvaju u folderu `Data` pored izvršnog fajla
- Arhitektura: model, kontroler i prikaz (MVC)

## Struktura projekta
```
Core/
  Models/       klase modela (Posetilac, Autor, Knjiga, Kupovina, Izdavac, Adresa, StatusClanstva)
  Controller/   poslovna logika i validacija
  View/         konzolni prikaz i unos
  Storage/      čuvanje i učitavanje (CSV), povezivanje objekata
  Program.cs    ulazna tačka
```

## Pokretanje
1. Klonirati repozitorijum.
2. Otvoriti `.sln` fajl u Visual Studiju.
3. Pokrenuti projekat (F5).

Podaci se učitavaju pri pokretanju, a čuvaju se nakon svake izmene i pri izlasku iz aplikacije.

## Napomene
- Veza posetilac-adresa i autor-adresa je 1-1.
- Brisanje posetioca briše i njegove kupovine. Brisanje knjige briše njene kupovine i uklanja je sa lista želja. Brisanje autora nije dozvoljeno ako je jedini autor neke knjige.
- Šef izdavača mora biti autor sa najmanje 5 godina iskustva.