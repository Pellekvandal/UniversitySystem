using System.Collections.Generic;

public class Faglærer : Bruker
{
    public string Avdeling { get; init; }
    public List<string> UndervisningsKurs { get; } = new(); // kurskoder faglærer underviser

    public Faglærer(string brukernavn, string passord, string navn, string epost, string avdeling)
        : base(brukernavn, passord, navn, epost, Rolle.Faglærer)
    {
        Avdeling = avdeling;
    }

    public void OpprettKurs(List<Kurs> kursListe, string kode, string navn, int studiepoeng, int maksPlasser)
    {
        foreach (Kurs k in kursListe)
        {
            if (k.Kode.Equals(kode, System.StringComparison.OrdinalIgnoreCase) ||
                k.Navn.Equals(navn, System.StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Et kurs med kode '{kode}' eller navn '{navn}' finnes allerede.");
        }

        Kurs nyttKurs = new Kurs(kode, navn, studiepoeng, maksPlasser, Id);
        kursListe.Add(nyttKurs);
        UndervisningsKurs.Add(kode);
    }

    public void RegistrerPensum(Kurs kurs, string pensum)
    {
        if (kurs.FaglærerId != Id)
            throw new UnauthorizedAccessException("Du underviser ikke dette kurset.");

        kurs.Pensum = pensum;
    }

    public void SettKarakter(Kurs kurs, Student student, string karakter, List<string> gyldigeKarakterer)
    {
        if (kurs.FaglærerId != Id)
            throw new UnauthorizedAccessException("Du underviser ikke dette kurset.");

        if (!kurs.Studenter.Contains(student.Id))
            throw new InvalidOperationException("Studenten er ikke påmeldt dette kurset.");

        if (!gyldigeKarakterer.Contains(karakter))
            throw new ArgumentException($"Ugyldig karakter. Gyldige verdier: {string.Join(", ", gyldigeKarakterer)}");

        student.SettKarakter(kurs.Kode, karakter);
    }
}