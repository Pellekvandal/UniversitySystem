using System;
using System.Collections.Generic;

class Program
{
    static List<Kurs> kursListe = new();
    static List<Bok> bokListe = new();
    static LånTjeneste lånTjeneste = new();
    static BrukersystemTjeneste brukersystem = new();

    static readonly List<string> GyldigeKarakterer = new() { "A", "B", "C", "D", "E", "F" };

    static void Main()
    {
        brukersystem.LastTestdata();
        SeedBøker();

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("=== Universitetssystem ===");
            Console.WriteLine("[1] Logg inn");
            Console.WriteLine("[2] Registrer ny bruker");
            Console.WriteLine("[0] Avslutt");

            if (!InputHelper.PrøvLesEnum<StartValg>("Velg: ", out StartValg startValg))
            {
                Console.WriteLine("Ugyldig valg.");
                continue;
            }

            switch (startValg)
            {
                case StartValg.LoggInn:
                    HåndterInnlogging();
                    break;

                case StartValg.Registrer:
                    HåndterRegistrering();
                    break;

                case StartValg.Avslutt:
                    running = false;
                    Console.WriteLine("Programmet avsluttes.");
                    break;
            }
        }
    }

    // ── INNLOGGING ────────────────────────────────────────────────

    static void HåndterInnlogging()
    {
        try
        {
            string brukernavn = InputHelper.LesString("Brukernavn: ");
            string passord = InputHelper.LesString("Passord: ");

            Bruker? bruker = brukersystem.LoggInn(brukernavn, passord);

            if (bruker == null)
            {
                Console.WriteLine("Feil brukernavn eller passord.");
                return;
            }

            Console.WriteLine($"\nVelkommen, {bruker.Navn}!");

            switch (bruker.Rolle)
            {
                case Rolle.Student:
                case Rolle.Utvekslingsstudent:
                    StudentMeny((Student)bruker);
                    break;

                case Rolle.Faglærer:
                    FaglærerMeny((Faglærer)bruker);
                    break;

                case Rolle.Bibliotekar:
                    BibliotekarMeny((Bibliotekar)bruker);
                    break;
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Feil: {ex.Message}");
        }
    }

    // ── REGISTRERING ──────────────────────────────────────────────

    static void HåndterRegistrering()
    {
        try
        {
            Console.WriteLine("Hvilken rolle?");
            Console.WriteLine("[1] Student");
            Console.WriteLine("[2] Faglærer");
            Console.WriteLine("[3] Bibliotekar");

            if (!InputHelper.PrøvLesEnum<Rolle>("Velg rolle: ", out Rolle rolle))
            {
                Console.WriteLine("Ugyldig valg.");
                return;
            }

            string brukernavn = InputHelper.LesString("Velg brukernavn: ");
            string passord = InputHelper.LesString("Velg passord: ");
            string navn = InputHelper.LesString("Fullt navn: ");
            string epost = InputHelper.LesString("Epost: ");

            switch (rolle)
            {
                case Rolle.Student:
                    brukersystem.RegistrerStudent(brukernavn, passord, navn, epost);
                    Console.WriteLine("Student registrert!");
                    break;

                case Rolle.Faglærer:
                    string avdeling = InputHelper.LesString("Avdeling: ");
                    brukersystem.RegistrerFaglærer(brukernavn, passord, navn, epost, avdeling);
                    Console.WriteLine("Faglærer registrert!");
                    break;

                case Rolle.Bibliotekar:
                    brukersystem.RegistrerBibliotekar(brukernavn, passord, navn, epost);
                    Console.WriteLine("Bibliotekar registrert!");
                    break;

                default:
                    Console.WriteLine("Ugyldig rolle.");
                    break;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Registrering feilet: {ex.Message}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Feil: {ex.Message}");
        }
    }

    // ── STUDENT-MENY ──────────────────────────────────────────────

    static void StudentMeny(Student student)
    {
        bool loggetInn = true;
        while (loggetInn)
        {
            Console.WriteLine();
            Console.WriteLine($"--- Studentmeny ({student.Navn}) ---");
            Console.WriteLine("[1] Meld på kurs");
            Console.WriteLine("[2] Meld av kurs");
            Console.WriteLine("[3] Vis mine kurs og karakterer");
            Console.WriteLine("[4] Søk på bok");
            Console.WriteLine("[5] Lån bok");
            Console.WriteLine("[6] Returner bok");
            Console.WriteLine("[0] Logg ut");

            if (!InputHelper.PrøvLesEnum<StudentValg>("Velg: ", out StudentValg valg))
            {
                Console.WriteLine("Ugyldig valg.");
                continue;
            }

            try
            {
                switch (valg)
                {
                    case StudentValg.MeldPaaKurs:
                    {
                        string kode = InputHelper.LesString("Kurskode: ");
                        Kurs? kurs = KursMenyHjelp.FinnKurs(kursListe, kode);
                        if (kurs == null) { Console.WriteLine("Fant ikke kurs."); break; }
                        student.MeldPaaKurs(kurs);
                        Console.WriteLine($"Du er meldt på '{kurs.Navn}'.");
                        break;
                    }

                    case StudentValg.MeldAvKurs:
                    {
                        string kode = InputHelper.LesString("Kurskode: ");
                        Kurs? kurs = KursMenyHjelp.FinnKurs(kursListe, kode);
                        if (kurs == null) { Console.WriteLine("Fant ikke kurs."); break; }
                        student.MeldAvKurs(kurs);
                        Console.WriteLine($"Du er meldt av '{kurs.Navn}'.");
                        break;
                    }

                    case StudentValg.VisKurserOgKarakterer:
                        Console.WriteLine("Dine kurs:");
                        student.VisKurserOgKarakterer();
                        break;

                    case StudentValg.SøkBok:
                        BokMenyHjelp.SøkBok(bokListe);
                        break;

                    case StudentValg.LånBok:
                        BokMenyHjelp.LånBok(bokListe, lånTjeneste, student.Id, student.Navn);
                        break;

                    case StudentValg.ReturnerBok:
                        BokMenyHjelp.ReturnerBok(bokListe, lånTjeneste, student.Id);
                        break;

                    case StudentValg.Logg_ut:
                        loggetInn = false;
                        Console.WriteLine("Logget ut.");
                        break;
                }
            }
            catch (InvalidOperationException ex) { Console.WriteLine($"Feil: {ex.Message}"); }
            catch (KeyNotFoundException ex) { Console.WriteLine($"Feil: {ex.Message}"); }
            catch (ArgumentException ex) { Console.WriteLine($"Feil: {ex.Message}"); }
        }
    }

    // ── FAGLÆRER-MENY ─────────────────────────────────────────────

    static void FaglærerMeny(Faglærer lærer)
    {
        bool loggetInn = true;
        while (loggetInn)
        {
            Console.WriteLine();
            Console.WriteLine($"--- Faglærermeny ({lærer.Navn}) ---");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Søk på kurs");
            Console.WriteLine("[3] Søk på bok");
            Console.WriteLine("[4] Lån bok");
            Console.WriteLine("[5] Returner bok");
            Console.WriteLine("[6] Sett karakter");
            Console.WriteLine("[7] Registrer pensum");
            Console.WriteLine("[8] Vis mine kurs");
            Console.WriteLine("[0] Logg ut");

            if (!InputHelper.PrøvLesEnum<FaglærerValg>("Velg: ", out FaglærerValg valg))
            {
                Console.WriteLine("Ugyldig valg.");
                continue;
            }

            try
            {
                switch (valg)
                {
                    case FaglærerValg.OpprettKurs:
                    {
                        string kode = InputHelper.LesString("Kurskode: ");
                        string navn = InputHelper.LesString("Kursnavn: ");
                        int sp = InputHelper.LesInt("Studiepoeng: ");
                        int maks = InputHelper.LesInt("Maks plasser: ");
                        lærer.OpprettKurs(kursListe, kode, navn, sp, maks);
                        Console.WriteLine("Kurs opprettet.");
                        break;
                    }

                    case FaglærerValg.SøkKurs:
                        KursMenyHjelp.SøkKurs(kursListe);
                        break;

                    case FaglærerValg.SøkBok:
                        BokMenyHjelp.SøkBok(bokListe);
                        break;

                    case FaglærerValg.LånBok:
                        BokMenyHjelp.LånBok(bokListe, lånTjeneste, lærer.Id, lærer.Navn);
                        break;

                    case FaglærerValg.ReturnerBok:
                        BokMenyHjelp.ReturnerBok(bokListe, lånTjeneste, lærer.Id);
                        break;

                    case FaglærerValg.SettKarakter:
                    {
                        string kurskode = InputHelper.LesString("Kurskode: ");
                        Kurs? kurs = KursMenyHjelp.FinnKurs(kursListe, kurskode);
                        if (kurs == null) { Console.WriteLine("Fant ikke kurs."); break; }

                        // Vis studenter i kurset
                        Console.WriteLine("Studenter i kurset:");
                        foreach (Bruker b in brukersystem.HentAlleBrukere())
                        {
                            if (b is Student s && kurs.Studenter.Contains(s.Id))
                                Console.WriteLine($"  [{s.Id}] {s.Navn}");
                        }

                        int studentId = InputHelper.LesInt("StudentID: ");
                        Student? student = null;
                        foreach (Bruker b in brukersystem.HentAlleBrukere())
                        {
                            if (b is Student s && s.Id == studentId)
                            {
                                student = s;
                                break;
                            }
                        }

                        if (student == null) { Console.WriteLine("Fant ikke student."); break; }

                        Console.WriteLine($"Gyldige karakterer: {string.Join(", ", GyldigeKarakterer)}");
                        string karakter = InputHelper.LesString("Karakter: ").ToUpper();
                        lærer.SettKarakter(kurs, student, karakter, GyldigeKarakterer);
                        Console.WriteLine($"Karakter {karakter} satt for {student.Navn}.");
                        break;
                    }

                    case FaglærerValg.RegistrerPensum:
                    {
                        string kurskode = InputHelper.LesString("Kurskode: ");
                        Kurs? kurs = KursMenyHjelp.FinnKurs(kursListe, kurskode);
                        if (kurs == null) { Console.WriteLine("Fant ikke kurs."); break; }
                        string pensum = InputHelper.LesString("Pensum (beskriv): ");
                        lærer.RegistrerPensum(kurs, pensum);
                        Console.WriteLine("Pensum registrert.");
                        break;
                    }

                    case FaglærerValg.VisMineKurs:
                        if (lærer.UndervisningsKurs.Count == 0)
                        {
                            Console.WriteLine("Du har ingen kurs.");
                            break;
                        }
                        foreach (string kode in lærer.UndervisningsKurs)
                        {
                            Kurs? k = KursMenyHjelp.FinnKurs(kursListe, kode);
                            if (k != null)
                                Console.WriteLine($"  [{k.Kode}] {k.Navn} — {k.Studenter.Count}/{k.MaksPlasser} studenter");
                        }
                        break;

                    case FaglærerValg.Logg_ut:
                        loggetInn = false;
                        Console.WriteLine("Logget ut.");
                        break;
                }
            }
            catch (InvalidOperationException ex) { Console.WriteLine($"Feil: {ex.Message}"); }
            catch (UnauthorizedAccessException ex) { Console.WriteLine($"Tilgang nektet: {ex.Message}"); }
            catch (KeyNotFoundException ex) { Console.WriteLine($"Feil: {ex.Message}"); }
            catch (ArgumentException ex) { Console.WriteLine($"Feil: {ex.Message}"); }
        }
    }

    // ── BIBLIOTEKAR-MENY ──────────────────────────────────────────

    static void BibliotekarMeny(Bibliotekar bibliotekar)
    {
        bool loggetInn = true;
        while (loggetInn)
        {
            Console.WriteLine();
            Console.WriteLine($"--- Bibliotekarmeny ({bibliotekar.Navn}) ---");
            Console.WriteLine("[1] Registrer bok");
            Console.WriteLine("[2] Vis aktive lån");
            Console.WriteLine("[3] Vis historikk");
            Console.WriteLine("[0] Logg ut");

            if (!InputHelper.PrøvLesEnum<BibliotekarValg>("Velg: ", out BibliotekarValg valg))
            {
                Console.WriteLine("Ugyldig valg.");
                continue;
            }

            try
            {
                switch (valg)
                {
                    case BibliotekarValg.RegistrerBok:
                    {
                        string tittel = InputHelper.LesString("Tittel: ");
                        string forfatter = InputHelper.LesString("Forfatter: ");
                        int aar = InputHelper.LesInt("Utgivelsesår: ");
                        int antall = InputHelper.LesInt("Antall eksemplarer: ");
                        Bok nyBok = bibliotekar.RegistrerBok(bokListe, tittel, forfatter, aar, antall);
                        Console.WriteLine($"Bok registrert med ID {nyBok.Id}.");
                        break;
                    }

                    case BibliotekarValg.VisAktiveLaan:
                        Console.WriteLine("Aktive lån:");
                        bibliotekar.VisAktiveLaan(lånTjeneste.HentAlleLaan());
                        break;

                    case BibliotekarValg.VisHistorikk:
                        Console.WriteLine("Lånehistorikk:");
                        bibliotekar.VisHistorikk(lånTjeneste.HentAlleLaan());
                        break;

                    case BibliotekarValg.Logg_ut:
                        loggetInn = false;
                        Console.WriteLine("Logget ut.");
                        break;
                }
            }
            catch (ArgumentException ex) { Console.WriteLine($"Feil: {ex.Message}"); }
            catch (FormatException ex) { Console.WriteLine($"Ugyldig input: {ex.Message}"); }
        }
    }

    // ── SEED DATA ─────────────────────────────────────────────────

    static void SeedBøker()
    {
        bokListe.Add(new Bok("Algoritmer og datastrukturer", "Thomas Cormen", 2009, 3));
        bokListe.Add(new Bok("Clean Code", "Robert C. Martin", 2008, 2));
        bokListe.Add(new Bok("Objektorientert programmering", "Bjarne Stroustrup", 2013, 4));
    }
}
