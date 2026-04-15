public class Bok
{
    private static int _nesteId = 0;
    public int Id { get; init; }
    public string Tittel { get; init; }
    public string Forfatter { get; init; }
    public int Aar { get; init; }
    public int AntallEksemplarer { get; init; }
    public int TilgjengeligeEksemplarer { get; private set; }

    public Bok(string tittel, string forfatter, int aar, int antallEksemplarer)
    {
        Id = ++_nesteId;
        Tittel = tittel;
        Forfatter = forfatter;
        Aar = aar;
        AntallEksemplarer = antallEksemplarer;
        TilgjengeligeEksemplarer = antallEksemplarer;
    }

    public void LånUt()
    {
        if (TilgjengeligeEksemplarer <= 0)
            throw new System.InvalidOperationException("Ingen tilgjengelige eksemplarer.");
        TilgjengeligeEksemplarer--;
    }

    public void Returner()
    {
        if (TilgjengeligeEksemplarer >= AntallEksemplarer)
            throw new System.InvalidOperationException("Alle eksemplarer er allerede tilgjengelige.");
        TilgjengeligeEksemplarer++;
    }
}
