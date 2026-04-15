using System;
using System.Collections.Generic;

public enum StartValg { LoggInn = 1, Registrer = 2, Avslutt = 0 }
public enum StudentValg { MeldPaaKurs = 1, MeldAvKurs = 2, VisKurserOgKarakterer = 3, SøkBok = 4, LånBok = 5, ReturnerBok = 6, Logg_ut = 0 }
public enum FaglærerValg { OpprettKurs = 1, SøkKurs = 2, SøkBok = 3, LånBok = 4, ReturnerBok = 5, SettKarakter = 6, RegistrerPensum = 7, VisMineKurs = 8, Logg_ut = 0 }
public enum BibliotekarValg { RegistrerBok = 1, VisAktiveLaan = 2, VisHistorikk = 3, Logg_ut = 0 }

public static class InputHelper
{
    public static string LesString(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Input kan ikke være tom.");
        return input.Trim();
    }

    public static int LesInt(string prompt)
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (!int.TryParse(input, out int resultat))
            throw new FormatException($"'{input}' er ikke et gyldig heltall.");
        return resultat;
    }

    public static bool PrøvLesEnum<T>(string prompt, out T resultat) where T : struct, Enum
    {
        Console.Write(prompt);
        string? input = Console.ReadLine();
        if (int.TryParse(input, out int tall) && Enum.IsDefined(typeof(T), tall))
        {
            resultat = (T)(object)tall;
            return true;
        }
        resultat = default;
        return false;
    }
}
