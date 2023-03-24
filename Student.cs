public class Student
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public DateTime Date { get; set; }
    public string Gender { get; set; }
    public string Faculty { get; set; }

    public Student()
    {
        ID = 0;
        Name = "";
        Date = DateTime.MinValue;
        Gender = "";
        Faculty = "";
    }
}
