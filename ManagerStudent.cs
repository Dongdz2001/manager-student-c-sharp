class StudentManager
{
    private List<Student> students = new List<Student>();

    public void AddStudent(Student? student)
    {
        students.Add(student!);
    }

    public void EditStudent(int id, Student? student)
    {
        students[this.getIndexStudentInListByID(id)] = student!;
    }
    public int getIndexStudentInListByID(int id)
    {
        int index = -1;
        for (int i = 0; i < students.Count; i++)
        {
            if (students[i].ID == id)
            {
                index = i;
                break;
            }
        }
        return index;
    }


    public void DeleteStudent(int id)
    {
        students!.RemoveAt(this.getIndexStudentInListByID(id));
    }

    public Student GetStudentsByID(int id)
    {
        foreach (Student student in students)
        {
            if (student.ID == id)
            {
                return student;
            }
        }
        return null!;
    }


    public bool IsStudentExist(int studentID)
    {
        return students.Exists(s => s.ID == studentID);
    }


    public List<Student> SearchStudents(string searchName)
    {
        if (searchName != null)
        {
            List<Student> results = this.students.FindAll(s => s.Name != null && s.Name.ToLower().Contains(searchName.ToLower()));
            return results;
        }
        return new List<Student>();
    }
    public List<Student> GetAllStudents()
    {
        return students;
    }


    public int Count()
    {
        return students.Count;
    }
}
