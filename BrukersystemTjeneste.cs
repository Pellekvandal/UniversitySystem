using System;
using System.Collections.Generic;

public class BrukersystemTjeneste
{
    private List<Bruker> _brukere = new();

    // Seed testdata
    public void LastTestdata()
    {
        // Studenter
        _brukere.Add(new Student("pelle", "1234", "Pelle Hansen", "pelle@mail.no"));
        _brukere.Add(new Student("mads", "1234", "Mads Olsen", "mads@mail.no"));
        _brukere.Add(new Utvekslingsstudent("elis", "1234", "Elis Svensson", "elis@mail.no",
            "Lund University", "Sverige", "01.08.2026", "15.12.2026"));

        // Faglærere
        _brukere.Add(new Faglærer("lars", "1234", "Lars Berg", "lars@uni.no", "Informatikk"));
        _brukere.Add(new Faglærer("anna", "1234", "Anna Lie", "anna@uni.no", "Økonomi"));

        // Bibliotekar
        _brukere.Add(new Bibliotekar("emilie", "1234", "Emilie Dahl", "emilie@uni.no"));
    }

    public bool BrukernavnFinnes(string brukernavn)
    {
        foreach (Bruker b in _brukere)
        {
            if (b.Brukernavn.Equals(brukernavn, StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    public Bruker? LoggInn(string brukernavn, string passord)
    {
        foreach (Bruker b in _brukere)
        {
            if (b.Brukernavn.Equals(brukernavn, StringComparison.OrdinalIgnoreCase) &&
                b.SjekkPassord(passord))
                return b;
        }
        return null;
    }

    public Student RegistrerStudent(string brukernavn, string passord, string navn, string epost)
    {
        if (BrukernavnFinnes(brukernavn))
            throw new InvalidOperationException("Brukernavnet er allerede i bruk.");

        Student ny = new Student(brukernavn, passord, navn, epost);
        _brukere.Add(ny);
        return ny;
    }

    public Faglærer RegistrerFaglærer(string brukernavn, string passord, string navn, string epost, string avdeling)
    {
        if (BrukernavnFinnes(brukernavn))
            throw new InvalidOperationException("Brukernavnet er allerede i bruk.");

        Faglærer ny = new Faglærer(brukernavn, passord, navn, epost, avdeling);
        _brukere.Add(ny);
        return ny;
    }

    public Bibliotekar RegistrerBibliotekar(string brukernavn, string passord, string navn, string epost)
    {
        if (BrukernavnFinnes(brukernavn))
            throw new InvalidOperationException("Brukernavnet er allerede i bruk.");

        Bibliotekar ny = new Bibliotekar(brukernavn, passord, navn, epost);
        _brukere.Add(ny);
        return ny;
    }

    public List<Bruker> HentAlleBrukere() => _brukere;
}