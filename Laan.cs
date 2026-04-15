public class Laan
{
    private static int _nesteId = 0;
    public int LaanId { get; init; }
    public int BokId { get; init; }
    public int LånerId { get; init; }
    public System.DateTime LaanDato { get; init; }
    public System.DateTime? ReturnertDato { get; private set; }

    public Laan(int bokId, int lånerId)
    {
        LaanId = ++_nesteId;
        BokId = bokId;
        LånerId = lånerId;
        LaanDato = System.DateTime.Now;
        ReturnertDato = null;
    }

    public void RegistrerRetur()
    {
        if (ReturnertDato != null)
            throw new System.InvalidOperationException("Boken er allerede returnert.");
        ReturnertDato = System.DateTime.Now;
    }

    public bool ErAktiv() => ReturnertDato == null;
}
