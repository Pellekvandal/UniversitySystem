using Xunit;
using System.Collections.Generic;

public class UniversitySystemTests
{
    [Fact]
    public void Student_KanIkkeMeldePaaSammeKursTwoGanger()
    {
        // Arrange
        Student student = new Student("test", "pass", "Test Student", "test@mail.no");
        Faglærer lærer = new Faglærer("lærer", "pass", "Test Lærer", "lærer@uni.no", "IT");
        List<Kurs> kursListe = new();
        lærer.OpprettKurs(kursListe, "INF101", "Intro til programmering", 10, 30);
        Kurs kurs = kursListe[0];

        // Act
        student.MeldPaaKurs(kurs);

        // Assert
        Assert.Throws<InvalidOperationException>(() => student.MeldPaaKurs(kurs));
    }

    [Fact]
    public void Faglærer_KanIkkeOppretteDuplikatKurs()
    {
        // Arrange
        Faglærer lærer = new Faglærer("lærer2", "pass", "Lærer To", "l2@uni.no", "Matte");
        List<Kurs> kursListe = new();
        lærer.OpprettKurs(kursListe, "MAT201", "Lineær Algebra", 10, 25);

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            lærer.OpprettKurs(kursListe, "MAT201", "Et annet navn", 10, 25));
    }

    [Fact]
    public void Bok_KanIkkeLånesUtNårIngenEksemplarer()
    {
        // Arrange
        Bok bok = new Bok("Testbok", "Forfatter", 2020, 1);
        LånTjeneste lånTjeneste = new LånTjeneste();
        Student student1 = new Student("låner1", "pass", "Låner En", "en@mail.no");
        Student student2 = new Student("låner2", "pass", "Låner To", "to@mail.no");

        // Act
        lånTjeneste.LånBok(bok, student1.Id);

        // Assert
        Assert.Throws<InvalidOperationException>(() => lånTjeneste.LånBok(bok, student2.Id));
    }

    [Fact]
    public void Faglærer_KanSetteKarakterOgStudentHenterRiktigVerdi()
    {
        // Arrange
        Faglærer lærer = new Faglærer("lærer3", "pass", "Lærer Tre", "l3@uni.no", "Norsk");
        Student student = new Student("student1", "pass", "Test Student", "s@mail.no");
        List<Kurs> kursListe = new();
        List<string> gyldige = new() { "A", "B", "C", "D", "E", "F" };
        lærer.OpprettKurs(kursListe, "NOR301", "Norsk litteratur", 10, 20);
        Kurs kurs = kursListe[0];
        student.MeldPaaKurs(kurs);

        // Act
        lærer.SettKarakter(kurs, student, "B", gyldige);

        // Assert
        Assert.Equal("B", student.Karakterer["NOR301"]);
    }
}
