public class Utvekslingsstudent : Student
{
    public string Hjemuniversitet { get; init; }
    public string Land { get; init; }
    public string PeriodeFra { get; init; }
    public string PeriodeTil { get; init; }

    public Utvekslingsstudent(string brukernavn, string passord, string navn, string epost,
        string hjemuniversitet, string land, string periodeFra, string periodeTil)
        : base(brukernavn, passord, navn, epost)
    {
        Hjemuniversitet = hjemuniversitet;
        Land = land;
        PeriodeFra = periodeFra;
        PeriodeTil = periodeTil;
    }
}
