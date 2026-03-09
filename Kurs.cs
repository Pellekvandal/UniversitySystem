using System;
using System.Collections.Generic;
public class Kurs
{
public string Kode { get; set; }
public string Navn { get; set; }
public int Studiepoeng { get; set; }
public int MaksPlasser { get; set; }
public List<int> Studenter { get; set; }

public Kurs(string kode, string navn, int studiepoeng, int maksPlasser)
{
    Kode = kode;
    Navn = navn;
    Studiepoeng = studiepoeng;
    MaksPlasser = maksPlasser;
    Studenter = new List<int>();
}

public bool HarPlass()
{
    return Studenter.Count < MaksPlasser;
}
}
