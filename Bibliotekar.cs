using System.Collections.Generic;

public class Bibliotekar : Bruker
{
    public Bibliotekar(string brukernavn, string passord, string navn, string epost)
        : base(brukernavn, passord, navn, epost, Rolle.Bibliotekar) { }

    public Bok RegistrerBok(List<Bok> bokListe, string tittel, string forfatter, int aar, int antall)
    {
        Bok nyBok = new Bok(tittel, forfatter, aar, antall);
        bokListe.Add(nyBok);
        return nyBok;
    }

    public void VisAktiveLaan(List<Laan> laanListe)
    {
        bool noen = false;
        foreach (Laan l in laanListe)
        {
            if (l.ReturnertDato == null)
            {
                System.Console.WriteLine($"  BokID: {l.BokId} | LånerID: {l.LånerId} | Lånt: {l.LaanDato:dd.MM.yyyy}");
                noen = true;
            }
        }
        if (!noen) System.Console.WriteLine("  Ingen aktive lån.");
    }

    public void VisHistorikk(List<Laan> laanListe)
    {
        if (laanListe.Count == 0) { System.Console.WriteLine("  Ingen lån registrert."); return; }
        foreach (Laan l in laanListe)
        {
            string status = l.ReturnertDato == null ? "Aktiv" : $"Returnert: {l.ReturnertDato:dd.MM.yyyy}";
            System.Console.WriteLine($"  BokID: {l.BokId} | LånerID: {l.LånerId} | Lånt: {l.LaanDato:dd.MM.yyyy} | {status}");
        }
    }
}
