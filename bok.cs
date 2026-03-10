public class Bok
{
    public int Id { get; set; }
    public string Tittel { get; set; }
    public string Forfatter { get; set; }
    public int Aar { get; set; }
    public int AntallEksemplarer { get; set; }
    public int TilgjengeligeEksemplarer { get; set; }

    public Bok(int id, string tittel, string forfatter, int aar, int antallEksemplarer)
    {
        Id = id;
        Tittel = tittel;
        Forfatter = forfatter;
        Aar = aar;
        AntallEksemplarer = antallEksemplarer;
        TilgjengeligeEksemplarer = antallEksemplarer;
    }
}