using System;
using System.Collections.Generic;

public class Student : Bruker
{
    public List<string> KursKoder { get; } = new();
    public Dictionary<string, string> Karakterer { get; } = new();

    public Student(string brukernavn, string passord, string navn, string epost)
        : base(brukernavn, passord, navn, epost, Rolle.Student) { }

    public void MeldPaaKurs(Kurs kurs)
    {
        if (KursKoder.Contains(kurs.Kode))
            throw new InvalidOperationException("Du er allerede meldt på dette kurset.");
        if (!kurs.HarPlass())
            throw new InvalidOperationException("Kurset er fullt.");
        kurs.LeggTilStudent(Id);
        KursKoder.Add(kurs.Kode);
    }

    public void MeldAvKurs(Kurs kurs)
    {
        if (!KursKoder.Contains(kurs.Kode))
            throw new InvalidOperationException("Du er ikke påmeldt dette kurset.");
        kurs.FjernStudent(Id);
        KursKoder.Remove(kurs.Kode);
    }

    public void SettKarakter(string kurskode, string karakter)
    {
        Karakterer[kurskode] = karakter;
    }

    public void VisKurserOgKarakterer()
    {
        if (KursKoder.Count == 0) { Console.WriteLine("Du er ikke meldt på noen kurs."); return; }
        foreach (string kode in KursKoder)
        {
            string karakter = Karakterer.ContainsKey(kode) ? Karakterer[kode] : "Ikke satt";
            Console.WriteLine($"  {kode} — Karakter: {karakter}");
        }
    }
}
