public enum Rolle { Student = 1, Faglærer = 2, Bibliotekar = 3, Utvekslingsstudent = 4 }

public abstract class Bruker
{
    private static int _nesteId = 1000;
    public int Id { get; init; }
    public string Brukernavn { get; init; }
    private string Passord { get; set; }
    public string Navn { get; init; }
    public string Epost { get; init; }
    public Rolle Rolle { get; init; }

    protected Bruker(string brukernavn, string passord, string navn, string epost, Rolle rolle)
    {
        Id = ++_nesteId;
        Brukernavn = brukernavn;
        Passord = passord;
        Navn = navn;
        Epost = epost;
        Rolle = rolle;
    }

    public bool SjekkPassord(string passord) => Passord == passord;
}
