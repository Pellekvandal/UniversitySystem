using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        bool running = true;

        List<Kurs> kursListe = new List<Kurs>();
        List<Student> studentListe = new List<Student>();

        studentListe.Add(new Student(1001, "Pelle", "pelle@mail.no"));
        studentListe.Add(new Student(1002, "Mads", "mads@mail.no"));

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
            string valg = Console.ReadLine() ?? "";

            switch (valg)
            {
                case "1":

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

                    Console.Write("Skriv studentID: ");
                    int studentId = int.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Skriv kurskode: ");
                    string kursKode = Console.ReadLine() ?? "";

                    Student valgtStudent = null;
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

                    if (!valgtKurs.HarPlass())
                    {
                        Console.WriteLine("Kurset er fullt.");
                        break;
                    }

                    if (valgtKurs.Studenter.Contains(valgtStudent.StudentId))
                    {
                        Console.WriteLine("Studenten er allerede meldt på.");
                        break;
                    }

                    valgtKurs.Studenter.Add(valgtStudent.StudentId);
                    valgtStudent.KursKoder.Add(valgtKurs.Kode);

                    Console.WriteLine("Studenten ble meldt på kurset.");
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
                                            Console.WriteLine("- " + s.StudentId + " " + s.Navn);
                                        }
                                    }
                                }
                            }

                            Console.WriteLine();
                        }
                    }

                    break;

                case "4":

                    Console.Write("Søk etter kurskode eller navn: ");
                    string søk = Console.ReadLine() ?? "";

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