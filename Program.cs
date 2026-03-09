using System;
using System.Collections.Generic;
class Program
{
static void Main()
{
bool running = true;
    List<Kurs> kursListe = new List<Kurs>();

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
        Console.WriteLine("[0] Avslutt");

        Console.Write("Velg et alternativ: ");
        string valg = Console.ReadLine();

        switch (valg)
        {
            case "1":

                Console.Write("Skriv kurskode: ");
                string kode = Console.ReadLine();

                Console.Write("Skriv kursnavn: ");
                string navn = Console.ReadLine();

                Console.Write("Studiepoeng: ");
                int studiepoeng = int.Parse(Console.ReadLine());

                Console.Write("Maks antall plasser: ");
                int maksPlasser = int.Parse(Console.ReadLine());

                Kurs nyttKurs = new Kurs(kode, navn, studiepoeng, maksPlasser);

                kursListe.Add(nyttKurs);

                Console.WriteLine("Kurset ble opprettet.");

                break;

            case "3":

                if (kursListe.Count == 0)
                {
                    Console.WriteLine("Ingen kurs registrert.");
                }
                else
                {
                    foreach (Kurs k in kursListe)
                    {
                        Console.WriteLine(k.Kode + " - " + k.Navn + " (" + k.Studiepoeng + " studiepoeng, maks " + k.MaksPlasser + ")");
                    }
                }

                break;

            case "4":

                Console.Write("Søk etter kurskode eller navn: ");
                string søk = Console.ReadLine();

                bool funnet = false;

                foreach (Kurs k in kursListe)
                {
                    if (k.Kode.Contains(søk, StringComparison.OrdinalIgnoreCase) ||
                        k.Navn.Contains(søk, StringComparison.OrdinalIgnoreCase))
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