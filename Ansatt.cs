public class Ansatt
{
    public int AnsattID { get; set; }
    public string Navn { get; set; }
    public string Epost { get; set; }
    public string Stilling { get; set; }
    public string Avdeling { get; set; }

    public Ansatt(int ansattID, string navn, string epost, string stilling, string avdeling)
    {
        AnsattID = ansattID;
        Navn = navn;
        Epost = epost;
        Stilling = stilling;
        Avdeling = avdeling;
    }
}