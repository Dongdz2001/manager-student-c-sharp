class HomeMain
{
    static void LineSpacing()
    {
        Console.WriteLine();
        Console.WriteLine("    ---------------------------------------------------------------------   ");
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        StudentManager studentManager = new StudentManager();
        studentManager.AddStudent(new Student { ID = 100, Name = "John", Date = (new DateTime(2000, 1, 1)), Gender = "Male", Faculty = "Science" });
        studentManager.AddStudent(new Student { ID = 101, Name = "Mary", Date = new DateTime(1998, 3, 15), Gender = "Female", Faculty = "Arts" });

        while (true)
        {
            Console.WriteLine("1. Xem danh sách sinh viên");
            Console.WriteLine("2. Thêm sinh viên");
            Console.WriteLine("3. Sửa thông tin sinh viên");
            Console.WriteLine("4. Xóa sinh viên");
            Console.WriteLine("5. Tìm kiếm sinh viên theo tên");
            Console.WriteLine("6. Thoát chương trình");
            Console.Write("Vui lòng chọn chức năng (1-6): ");
            string choice = Console.ReadLine()!;
            LineSpacing();
            switch (choice)
            {
                case "1":
                    // System.Diagnostics.Process.Start("CMD.exe", "/C clear");
                    List<Student> students = studentManager.GetAllStudents();
                    foreach (Student student in students)
                    {
                        Console.WriteLine(student.ID + " - " + student.Name + " - " + student.Date.ToString("dd/MM/yyyy") + " - " + student.Gender + " - " + student.Faculty);
                    }
                    break;
                case "2":
                    // System.Diagnostics.Process.Start("CMD.exe", "/C clear");

                    Console.Write("Nhập mã sinh viên: ");
                    int id = Convert.ToInt32(Console.ReadLine()!);
                    while(studentManager.IsStudentExist(id))
                    {
                        Console.Write("Mã sinh viên đã tồn tại hãy nhập lại mã sinh viên: ");
                        id = Convert.ToInt32(Console.ReadLine()!);
                    }
                    Console.Write("Nhập tên sinh viên: ");
                    string name = Console.ReadLine()!;
                    Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
                    DateTime date = DateTime.ParseExact(Console.ReadLine()!, "dd/MM/yyyy", null);
                    Console.Write("Nhập giới tính: ");
                    string gender = Console.ReadLine()!;
                    Console.Write("Nhập khoa: ");
                    string faculty = Console.ReadLine()!;
                    studentManager.AddStudent(new Student { ID = id, Name = name, Date = date, Gender = gender, Faculty = faculty });
                    Console.WriteLine("Thêm sinh viên thành công.");
                    break;
                case "3":
                    Console.Write("Nhập mã sinh viên cần sửa: ");
                    int id1 = Convert.ToInt32(Console.ReadLine()!);
                    if (!studentManager.IsStudentExist(id1))
                    {
                        Console.WriteLine("Mã sinh viên không hợp lệ hoặc không tồn tại!");
                        break;
                    }
                    Console.Write("Nhập tên sinh viên mới: ");
                    name = Console.ReadLine()!;
                    Console.Write("Nhập ngày sinh mới (dd/MM/yyyy): ");
                    String date1 = Console.ReadLine()!;
                    Console.Write("Nhập giới tính mới: ");
                    gender = Console.ReadLine()!;
                    Console.Write("Nhập khoa mới: ");
                    faculty = Console.ReadLine()!;

                    Student editStd = new Student();
                    Student tempStd = studentManager.GetStudentsByID(id1);
                    editStd.ID = tempStd.ID;
                    editStd.Name = !string.IsNullOrEmpty(name) ? name : tempStd.Name;
                    editStd.Gender = !string.IsNullOrEmpty(gender) ? gender : tempStd.Gender;
                    editStd.Date = date1 != "" ? DateTime.ParseExact(date1, "dd/MM/yyyy", null) : tempStd.Date;
                    editStd.Faculty = !string.IsNullOrEmpty(faculty) ? faculty : tempStd.Faculty;


                    studentManager.EditStudent(id1, editStd);
                    Console.WriteLine("Sửa thông tin sinh viên thành công.");
                    break;
                case "4":
                    // System.Diagnostics.Process.Start("CMD.exe", "/C clear");
                    Console.Write("Nhập mã sinh viên cần xóa: ");
                    int id2 = Convert.ToInt32(Console.ReadLine()!);
                    if (!studentManager.IsStudentExist(id2))
                    {
                        Console.WriteLine("Mã sinh viên không hợp lệ hoặc không tồn tại!");
                    }
                    else
                    {
                        studentManager.DeleteStudent(id2);
                        Console.WriteLine("Xóa sinh viên thành công!");
                    }
                    break;
                case "5":
                    // System.Diagnostics.Process.Start("CMD.exe", "/C clear");
                    Console.Write("Nhập tên sinh viên cần tìm kiếm: ");
                    string searchName = Console.ReadLine()!;
                    List<Student> searchResults = studentManager.SearchStudents(searchName);
                    if (searchResults.Count == 0)
                    {
                        Console.WriteLine("Không tìm thấy sinh viên có tên '{0}'", searchName);
                    }
                    else
                    {
                        LineSpacing();
                        Console.WriteLine("Kết quả tìm kiếm:");
                        foreach (Student student in searchResults)
                        {
                            Console.WriteLine(student.ID + " - " + student.Name + " - " + student.Date.ToString("dd/MM/yyyy") + " - " + student.Gender + " - " + student.Faculty);
                        }
                    }
                    break;
                case "6":
                    Console.WriteLine("Thoát chương trình.");
                    return;
                default:
                    // System.Diagnostics.Process.Start("CMD.exe", "/C clear");
                    Console.WriteLine("Vui lòng nhập lại lựa chọn.");
                    break;
            }
            LineSpacing();
        }
    }
}