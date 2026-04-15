using System;
using System.Collections.Generic;

public static class BokMenyHjelp
{
    public static void SøkBok(List<Bok> bokListe)
    {
        string søk = InputHelper.LesString("Søk etter tittel eller forfatter: ");
        bool funnet = false;
        foreach (Bok b in bokListe)
        {
            if (b.Tittel.Contains(søk, StringComparison.OrdinalIgnoreCase) ||
                b.Forfatter.Contains(søk, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"  [ID:{b.Id}] {b.Tittel} av {b.Forfatter} ({b.Aar}) — Tilgjengelig: {b.TilgjengeligeEksemplarer}/{b.AntallEksemplarer}");
                funnet = true;
            }
        }
        if (!funnet) Console.WriteLine("Ingen bøker funnet.");
    }

    public static void LånBok(List<Bok> bokListe, LånTjeneste lånTjeneste, int lånerId, string lånernavn)
    {
        int bokId = InputHelper.LesInt("Skriv bok-ID: ");
        Bok? valgtBok = null;
        foreach (Bok b in bokListe)
        {
            if (b.Id == bokId) { valgtBok = b; break; }
        }
        if (valgtBok == null)
            throw new System.Collections.Generic.KeyNotFoundException($"Fant ikke bok med ID {bokId}.");
        lånTjeneste.LånBok(valgtBok, lånerId);
        Console.WriteLine($"'{valgtBok.Tittel}' ble lånt ut til {lånernavn}.");
    }

    public static void ReturnerBok(List<Bok> bokListe, LånTjeneste lånTjeneste, int lånerId)
    {
        int bokId = InputHelper.LesInt("Skriv bok-ID du vil returnere: ");
        Bok? valgtBok = null;
        foreach (Bok b in bokListe)
        {
            if (b.Id == bokId) { valgtBok = b; break; }
        }
        if (valgtBok == null)
            throw new System.Collections.Generic.KeyNotFoundException($"Fant ikke bok med ID {bokId}.");
        lånTjeneste.ReturnerBok(valgtBok, lånerId);
        Console.WriteLine($"'{valgtBok.Tittel}' er returnert.");
    }
}

public static class KursMenyHjelp
{
    public static void SøkKurs(List<Kurs> kursListe)
    {
        string søk = InputHelper.LesString("Søk etter kurskode eller navn: ");
        bool funnet = false;
        foreach (Kurs k in kursListe)
        {
            if (k.Kode.Contains(søk, StringComparison.OrdinalIgnoreCase) ||
                k.Navn.Contains(søk, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"  [{k.Kode}] {k.Navn} — {k.Studiepoeng} sp — Plasser: {k.Studenter.Count}/{k.MaksPlasser}");
                if (!string.IsNullOrWhiteSpace(k.Pensum))
                    Console.WriteLine($"    Pensum: {k.Pensum}");
                funnet = true;
            }
        }
        if (!funnet) Console.WriteLine("Ingen kurs funnet.");
    }

    public static Kurs? FinnKurs(List<Kurs> kursListe, string kode)
    {
        foreach (Kurs k in kursListe)
        {
            if (k.Kode.Equals(kode, StringComparison.OrdinalIgnoreCase))
                return k;
        }
        return null;
    }
}
