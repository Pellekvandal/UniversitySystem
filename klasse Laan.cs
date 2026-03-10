public class Laan
{
public int BokId { get; set; }
public int StudentId { get; set; }
public DateTime LaanDato { get; set; }
public DateTime? ReturnertDato { get; set; }
public Laan(int bokId, int studentId)
{
    BokId = bokId;
    StudentId = studentId;
    LaanDato = DateTime.Now;
    ReturnertDato = null;
}
}
