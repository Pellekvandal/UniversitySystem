using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        bool running = true;

        // liste med alle kurs
        List<Kurs> kursListe = new List<Kurs>();

        // liste med alle studenter
        List<Student> studentListe = new List<Student>();

        // liste med alle ansatte
        List<Ansatt> ansattListe = new List<Ansatt>();

        // liste med alle bøker i biblioteket
        List<Bok> bokListe = new List<Bok>();

        // liste med alle lån
        List<Laan> laanListe = new List<Laan>();

        // teststudenter så vi kan melde dem på kurs
        studentListe.Add(new Student(1001, "Pelle", "pelle@mail.no"));
        studentListe.Add(new Student(1002, "Mads", "mads@mail.no"));

        // testansatte
        ansattListe.Add(new Ansatt(2001, "Emilie", "emilie@uni.no", "Bibliotekar", "Bibliotek"));
        ansattListe.Add(new Ansatt(2002, "Lars", "lars@uni.no", "Foreleser", "Informatikk"));

        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("--- Universitetssystem ---");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Meld student til kurs");
            Console.WriteLine("[3] Print kurs og deltagere");
            Console.WriteLine("[4] Søk på kurs");
            Console.WriteLine("[5] Søk på bok");
            Console.WriteLine("[6] Lån bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[8] Registrer bok");
            Console.WriteLine("[9] Vis lån og historikk");
            Console.WriteLine("[0] Avslutt");

            Console.Write("Velg et alternativ: ");
            string valg = Console.ReadLine() ?? "";

            switch (valg)
            {
                case "1":

                    // oppretter nytt kurs
                    Console.Write("Skriv kurskode: ");
                    string kode = Console.ReadLine() ?? "";

                    Console.Write("Skriv kursnavn: ");
                    string navn = Console.ReadLine() ?? "";

                    Console.Write("Studiepoeng: ");
                    int studiepoeng = int.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Maks antall plasser: ");
                    int maksPlasser = int.Parse(Console.ReadLine() ?? "0");

                    Kurs nyttKurs = new Kurs(kode, navn, studiepoeng, maksPlasser);
                    kursListe.Add(nyttKurs);

                    Console.WriteLine("Kurset ble opprettet.");
                    break;

                case "2":

                    // melder student til kurs
                    Console.Write("Skriv studentID: ");
                    int studentId = int.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Skriv kurskode: ");
                    string kursKode = Console.ReadLine() ?? "";

                    Student valgtStudent = null;

                    // finner riktig student
                    foreach (Student s in studentListe)
                    {
                        if (s.StudentId == studentId)
                        {
                            valgtStudent = s;
                            break;
                        }
                    }

                    if (valgtStudent == null)
                    {
                        Console.WriteLine("Fant ikke student.");
                        break;
                    }

                    Kurs valgtKurs = null;

                    // finner riktig kurs
                    foreach (Kurs k in kursListe)
                    {
                        if (k.Kode.Equals(kursKode, StringComparison.OrdinalIgnoreCase))
                        {
                            valgtKurs = k;
                            break;
                        }
                    }

                    if (valgtKurs == null)
                    {
                        Console.WriteLine("Fant ikke kurs.");
                        break;
                    }

                    // sjekker om kurset har plass
                    if (!valgtKurs.HarPlass())
                    {
                        Console.WriteLine("Kurset er fullt.");
                        break;
                    }

                    // sjekker om studenten allerede er påmeldt
                    if (valgtKurs.Studenter.Contains(valgtStudent.StudentId))
                    {
                        Console.WriteLine("Studenten er allerede meldt på.");
                        break;
                    }

                    // legger studenten på kurset
                    valgtKurs.Studenter.Add(valgtStudent.StudentId);
                    valgtStudent.KursKoder.Add(valgtKurs.Kode);

                    Console.WriteLine("Studenten ble meldt på kurset.");
                    break;

                case "3":

                    // printer alle kurs og deltakere
                    if (kursListe.Count == 0)
                    {
                        Console.WriteLine("Ingen kurs registrert.");
                    }
                    else
                    {
                        foreach (Kurs k in kursListe)
                        {
                            Console.WriteLine(k.Kode + " - " + k.Navn);

                            if (k.Studenter.Count == 0)
                            {
                                Console.WriteLine("Ingen deltakere.");
                            }
                            else
                            {
                                Console.WriteLine("Deltakere:");

                                foreach (int id in k.Studenter)
                                {
                                    foreach (Student s in studentListe)
                                    {
                                        if (s.StudentId == id)
                                        {
                                            Console.WriteLine("- " + s.Navn);
                                        }
                                    }
                                }
                            }

                            Console.WriteLine();
                        }
                    }

                    break;

                case "4":

                    // søker etter kurs
                    Console.Write("Søk etter kurskode eller navn: ");
                    string sok = Console.ReadLine() ?? "";

                    bool funnet = false;

                    foreach (Kurs k in kursListe)
                    {
                        if (k.Kode.Contains(sok, StringComparison.OrdinalIgnoreCase) ||
                            k.Navn.Contains(sok, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine(k.Kode + " - " + k.Navn);
                            funnet = true;
                        }
                    }

                    if (!funnet)
                    {
                        Console.WriteLine("Ingen kurs funnet.");
                    }

                    break;

                case "5":

                    // søker etter bok i biblioteket
                    Console.Write("Søk etter boktittel eller forfatter: ");
                    string bokSok = Console.ReadLine() ?? "";

                    bool bokFunnet = false;

                    foreach (Bok b in bokListe)
                    {
                        if (b.Tittel.Contains(bokSok, StringComparison.OrdinalIgnoreCase) ||
                            b.Forfatter.Contains(bokSok, StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine(b.Id + " - " + b.Tittel + " av " + b.Forfatter);
                            Console.WriteLine("Tilgjengelige: " + b.TilgjengeligeEksemplarer);
                            bokFunnet = true;
                        }
                    }

                    if (!bokFunnet)
                    {
                        Console.WriteLine("Ingen bøker funnet.");
                    }

                    break;

                case "6":

                    // velger om det er student eller ansatt som låner
                    Console.WriteLine("Hvem låner boka?");
                    Console.WriteLine("[1] Student");
                    Console.WriteLine("[2] Ansatt");
                    Console.Write("Velg: ");
                    string brukerTypeLaan = Console.ReadLine() ?? "";

                    int laanerId = 0;
                    bool gyldigBruker = false;
                    string laanerNavn = "";

                    if (brukerTypeLaan == "1")
                    {
                        Console.Write("Skriv studentID: ");
                        laanerId = int.Parse(Console.ReadLine() ?? "0");

                        foreach (Student s in studentListe)
                        {
                            if (s.StudentId == laanerId)
                            {
                                gyldigBruker = true;
                                laanerNavn = s.Navn;
                                break;
                            }
                        }
                    }
                    else if (brukerTypeLaan == "2")
                    {
                        Console.Write("Skriv ansattID: ");
                        laanerId = int.Parse(Console.ReadLine() ?? "0");

                        foreach (Ansatt a in ansattListe)
                        {
                            if (a.AnsattID == laanerId)
                            {
                                gyldigBruker = true;
                                laanerNavn = a.Navn;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ugyldig valg.");
                        break;
                    }

                    if (!gyldigBruker)
                    {
                        Console.WriteLine("Fant ikke bruker.");
                        break;
                    }

                    Console.Write("Skriv bok-ID: ");
                    int laanBokId = int.Parse(Console.ReadLine() ?? "0");

                    Bok valgtBok = null;

                    // finner boka som skal lånes
                    foreach (Bok b in bokListe)
                    {
                        if (b.Id == laanBokId)
                        {
                            valgtBok = b;
                            break;
                        }
                    }

                    if (valgtBok == null)
                    {
                        Console.WriteLine("Fant ikke bok.");
                        break;
                    }

                    // stopper utlån hvis ingen eksemplarer er ledige
                    if (valgtBok.TilgjengeligeEksemplarer <= 0)
                    {
                        Console.WriteLine("Ingen tilgjengelige eksemplarer.");
                        break;
                    }

                    // reduserer antall tilgjengelige og lagrer lånet
                    valgtBok.TilgjengeligeEksemplarer--;

                    Laan nyttLaan = new Laan(laanBokId, laanerId);
                    laanListe.Add(nyttLaan);

                    Console.WriteLine("Boken ble lånt ut til " + laanerNavn + ".");
                    break;

                case "7":

                    // velger om det er student eller ansatt som returnerer
                    Console.WriteLine("Hvem returnerer boka?");
                    Console.WriteLine("[1] Student");
                    Console.WriteLine("[2] Ansatt");
                    Console.Write("Velg: ");
                    string brukerTypeReturn = Console.ReadLine() ?? "";

                    int returnBrukerId = 0;
                    bool gyldigReturnBruker = false;

                    if (brukerTypeReturn == "1")
                    {
                        Console.Write("Skriv studentID: ");
                        returnBrukerId = int.Parse(Console.ReadLine() ?? "0");

                        foreach (Student s in studentListe)
                        {
                            if (s.StudentId == returnBrukerId)
                            {
                                gyldigReturnBruker = true;
                                break;
                            }
                        }
                    }
                    else if (brukerTypeReturn == "2")
                    {
                        Console.Write("Skriv ansattID: ");
                        returnBrukerId = int.Parse(Console.ReadLine() ?? "0");

                        foreach (Ansatt a in ansattListe)
                        {
                            if (a.AnsattID == returnBrukerId)
                            {
                                gyldigReturnBruker = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ugyldig valg.");
                        break;
                    }

                    if (!gyldigReturnBruker)
                    {
                        Console.WriteLine("Fant ikke bruker.");
                        break;
                    }

                    Console.Write("Skriv bok-ID: ");
                    int returnBokId = int.Parse(Console.ReadLine() ?? "0");

                    bool returnert = false;

                    // finner aktivt lån som matcher bruker og bok
                    foreach (Laan l in laanListe)
                    {
                        if (l.StudentId == returnBrukerId &&
                            l.BokId == returnBokId &&
                            l.ReturnertDato == null)
                        {
                            l.ReturnertDato = DateTime.Now;
                            returnert = true;

                            // øker antall tilgjengelige eksemplarer
                            foreach (Bok b in bokListe)
                            {
                                if (b.Id == returnBokId)
                                {
                                    b.TilgjengeligeEksemplarer++;
                                    break;
                                }
                            }

                            Console.WriteLine("Boken er returnert.");
                            break;
                        }
                    }

                    if (!returnert)
                    {
                        Console.WriteLine("Fant ikke aktivt lån.");
                    }

                    break;

                case "8":

                    // registrerer ny bok i biblioteket
                    Console.Write("Skriv bok-ID: ");
                    int bokId = int.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Skriv tittel: ");
                    string tittel = Console.ReadLine() ?? "";

                    Console.Write("Skriv forfatter: ");
                    string forfatter = Console.ReadLine() ?? "";

                    Console.Write("Skriv år: ");
                    int aar = int.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Antall eksemplarer: ");
                    int antall = int.Parse(Console.ReadLine() ?? "0");

                    Bok nyBok = new Bok(bokId, tittel, forfatter, aar, antall);
                    bokListe.Add(nyBok);

                    Console.WriteLine("Boken ble registrert.");
                    break;

                case "9":

                    // viser aktive lån og historikk
                    Console.WriteLine("Aktive lån:");

                    bool aktiveLaan = false;

                    foreach (Laan l in laanListe)
                    {
                        if (l.ReturnertDato == null)
                        {
                            Console.WriteLine("BokID: " + l.BokId + " StudentID: " + l.StudentId + " Lånt: " + l.LaanDato);
                            aktiveLaan = true;
                        }
                    }

                    if (!aktiveLaan)
                    {
                        Console.WriteLine("Ingen aktive lån.");
                    }

                    Console.WriteLine();
                    Console.WriteLine("Historikk:");

                    if (laanListe.Count == 0)
                    {
                        Console.WriteLine("Ingen lån registrert.");
                    }
                    else
                    {
                        foreach (Laan l in laanListe)
                        {
                            string status;

                            if (l.ReturnertDato == null)
                            {
                                status = "Aktiv";
                            }
                            else
                            {
                                status = "Returnert: " + l.ReturnertDato;
                            }

                            Console.WriteLine("BokID: " + l.BokId + " StudentID: " + l.StudentId + " Lånt: " + l.LaanDato + " Status: " + status);
                        }
                    }

                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Programmet avsluttes.");
                    break;

                default:
                    Console.WriteLine("Denne funksjonen er ikke laget enda.");
                    break;
            }
        }
    }
}