using MySql.Data.MySqlClient;
class HomeMain
{   
    static void DeleteStudentMySQL(int id, MySqlConnection connection){
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = @$"DELETE FROM student WHERE id = {id};";
        MySqlDataReader reader = command.ExecuteReader();
        connection.Close();
    }
    static void AddStudentMySQL(Student student, MySqlConnection connection){
        
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = @$"INSERT INTO student
                                VALUES ('{student.ID}', '{student.Name}', '{student.Age}', '{student.Gender}' ,'{ConvertDataSQL(student.Date)}', '{student.Faculty}');";  
        MySqlDataReader reader = command.ExecuteReader();      
        connection.Close();                     
    }
    static string ConvertDataSQL(DateTime date){
        string temp = date.ToString()!;
         string[] arr = temp.Split(' ')[0].Split('/');
         temp = $"{arr[2]}-{arr[0]}-{arr[1]} {DateTime.Now.ToString("HH:mm:ss").Split(' ')[0]}";
        return temp;
    }
    static void EditDatFromMySQL(Student student, MySqlConnection connection){
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = @$"UPDATE student
                                 SET name = '{student.Name}', age= {student.Age}, gender= '{student.Gender}', birthday= '{ConvertDataSQL(student.Date)}', faculty= '{student.Faculty}'
                                 WHERE id = {student.ID};";
        MySqlDataReader reader = command.ExecuteReader();
        connection.Close();
    }

    static void addDataFromMySQL(StudentManager studentManager, MySqlConnection connection)
    {
        connection.Open();
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM student";
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            object[] values = new object[reader.FieldCount];
            reader.GetValues(values);
            int id = Convert.ToInt32(values[0]);
            string name = values[1].ToString()!;
            string gender = values[3].ToString()!;
            int age = Convert.ToInt32(values[2]);
            // Console.WriteLine($"date1: {DateTime.Now.ToString("HH:mm:ss tt")}");
            String[] numDate = values[4].ToString()!.Split(' ')[0].Split('/');
            int[] numDateInt = Array.ConvertAll(numDate, s => Convert.ToInt32(s));
            // Console.WriteLine($"{numDateInt[0]} -- {numDateInt[1]} -- {numDateInt[2]}");
            DateTime date = new DateTime(numDateInt[2], numDateInt[0], numDateInt[1]);
            string faculty = values[5].ToString()!;
            studentManager.AddStudent(new Student { ID = id, Name = name,Age = age , Date = date, Gender = gender, Faculty = faculty });
        }
        connection.Close();
    }
    static void LineSpacing()
    {
        Console.WriteLine();
        Console.WriteLine("    ---------------------------------------------------------------------   ");
        Console.WriteLine();
    }
    static void Main(string[] args)
    {
        StudentManager studentManager = new StudentManager();

        string connectionString = "server=localhost; user id=root;password='';database=test";
        MySqlConnection connection = new MySqlConnection(connectionString);

        addDataFromMySQL(studentManager,connection);
        // studentManager.AddStudent(new Student { ID = 100, Name = "John", Date = (new DateTime(2000, 1, 1)), Gender = "Male", Faculty = "Science" });
        // studentManager.AddStudent(new Student { ID = 101, Name = "Mary", Date = new DateTime(1998, 3, 15), Gender = "Female", Faculty = "Arts" });

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
                    string format = "{0,-10}  {1,-10}  {2,-10}  {3,-15}  {4,-15} {5, -30}";
                    Console.WriteLine("         " + string.Format(format, "ID", "Name","Age" , "Date", "Gender", "Faculty"));
                    Console.WriteLine();
                    List<Student> students = studentManager.GetAllStudents();
                    foreach (Student student in students)
                    {
                        Console.WriteLine("         " + string.Format(format, student.ID, student.Name,student.Age ,student.Date.ToString("dd/MM/yyyy"), student.Gender, student.Faculty));
                    }

                    break;
                case "2":
                    // System.Diagnostics.Process.Start("CMD.exe", "/C clear");

                    Console.Write("Nhập mã sinh viên: ");
                    int id = Convert.ToInt32(Console.ReadLine()!);
                    while (studentManager.IsStudentExist(id))
                    {
                        Console.Write("Mã sinh viên đã tồn tại hãy nhập lại mã sinh viên: ");
                        id = Convert.ToInt32(Console.ReadLine()!);
                    }
                    Console.Write("Nhập tên sinh viên: ");
                    string name = Console.ReadLine()!;
                    Console.Write("Nhập tuổi sinh viên: ");
                    int age = Convert.ToInt32(Console.ReadLine()!);
                    Console.Write("Nhập ngày sinh (dd/MM/yyyy): ");
                    DateTime date = DateTime.ParseExact(Console.ReadLine()!, "dd/MM/yyyy", null);
                    Console.Write("Nhập giới tính: ");
                    string gender = Console.ReadLine()!;
                    Console.Write("Nhập khoa: ");
                    string faculty = Console.ReadLine()!;
                    Student newSt = new Student { ID = id, Name = name,Age = age , Date = date, Gender = gender, Faculty = faculty };
                    studentManager.AddStudent(newSt);
                    AddStudentMySQL(newSt,connection);
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
                    Console.Write("Nhập tuổi sinh viên mới: ");
                    age =  Convert.ToInt32(Console.ReadLine()!);
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
                    editStd.Age = age != 0 ? age : tempStd.Age;

                    EditDatFromMySQL(editStd,connection);
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
                        DeleteStudentMySQL(id2,connection);
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
                        Console.WriteLine();
                        string format1 = "{0,-10}  {1,-10}  {2,-10}  {3,-15}  {4,-15} {5, -30}";
                        Console.WriteLine("         " + string.Format(format1, "ID", "Name","Age" , "Date", "Gender", "Faculty"));
                        Console.WriteLine();
                        foreach (Student student in searchResults)
                        {
                            Console.WriteLine("         " + string.Format(format1, student.ID, student.Name,student.Age , student.Date.ToString("dd/MM/yyyy"), student.Gender, student.Faculty));
                        }
                    }
                    break;
                case "6":
                    Console.WriteLine("Thoát chương trình.");
                    return;
                default:
                    Console.WriteLine("Vui lòng nhập lại lựa chọn.");
                    break;
            }
            LineSpacing();
        }
    }
}