public class Utvekslingsstudent : Student
{
    public string Hjemuniversitet { get; set; }
    public string Land { get; set; }
    public string PeriodeFra { get; set; }
    public string PeriodeTil { get; set; }

    public Utvekslingsstudent(int studentId, string navn, string epost, string hjemuniversitet, string land, string periodeFra, string periodeTil)
        : base(studentId, navn, epost)
    {
        Hjemuniversitet = hjemuniversitet;
        Land = land;
        PeriodeFra = periodeFra;
        PeriodeTil = periodeTil;
    }
}