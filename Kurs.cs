using System;
using System.Collections.Generic;

public class Kurs
{
    private static int _nesteId = 100;
    public int KursId { get; init; }
    public string Kode { get; init; }
    public string Navn { get; init; }
    public int Studiepoeng { get; init; }
    public int MaksPlasser { get; init; }
    public int FaglærerId { get; init; }
    public string Pensum { get; set; } = "";
    public List<int> Studenter { get; } = new();

    public Kurs(string kode, string navn, int studiepoeng, int maksPlasser, int faglærerId)
    {
        KursId = ++_nesteId;
        Kode = kode;
        Navn = navn;
        Studiepoeng = studiepoeng;
        MaksPlasser = maksPlasser;
        FaglærerId = faglærerId;
    }

    public bool HarPlass() => Studenter.Count < MaksPlasser;

    public void LeggTilStudent(int studentId)
    {
        if (Studenter.Contains(studentId))
            throw new InvalidOperationException("Studenten er allerede meldt på kurset.");
        if (!HarPlass())
            throw new InvalidOperationException("Kurset er fullt.");
        Studenter.Add(studentId);
    }

    public void FjernStudent(int studentId)
    {
        if (!Studenter.Contains(studentId))
            throw new InvalidOperationException("Studenten er ikke påmeldt kurset.");
        Studenter.Remove(studentId);
    }
}
