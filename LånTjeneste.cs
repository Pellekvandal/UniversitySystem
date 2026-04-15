using System.Collections.Generic;

public class LånTjeneste
{
    private List<Laan> _laan = new();

    public Laan LånBok(Bok bok, int lånerId)
    {
        bok.LånUt(); // kaster exception hvis ikke tilgjengelig
        Laan nyttLaan = new Laan(bok.Id, lånerId);
        _laan.Add(nyttLaan);
        return nyttLaan;
    }

    public void ReturnerBok(Bok bok, int lånerId)
    {
        foreach (Laan l in _laan)
        {
            if (l.BokId == bok.Id && l.LånerId == lånerId && l.ErAktiv())
            {
                l.RegistrerRetur();
                bok.Returner();
                return;
            }
        }
        throw new InvalidOperationException("Fant ikke aktivt lån for denne brukeren og boken.");
    }

    public List<Laan> HentAlleLaan() => _laan;
}