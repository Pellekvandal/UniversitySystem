using System.Collections.Generic;

public class Student
{
    public int StudentId { get; set; }
    public string Navn { get; set; }
    public string Epost { get; set; }
    public List<string> KursKoder { get; set; }

    public Student(int studentId, string navn, string epost)
    {
        StudentId = studentId;
        Navn = navn;
        Epost = epost;
        KursKoder = new List<string>();
    }
}