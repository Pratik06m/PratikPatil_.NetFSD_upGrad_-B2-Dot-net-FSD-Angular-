using System;
using System.Collections.Generic;
using System.Linq;

[cite_start]// Entity Class [cite: 199]
[cite_start]
public class Student // [cite: 200]
{
    public int StudentId { get; set; } // [cite: 202]
    public string StudentName { get; set; } // [cite: 202]
    public string Course { get; set; } // [cite: 202]
}

[cite_start]// Repository Interface [cite: 203]
[cite_start]
public interface IStudentRepository // [cite: 204]
{
    void AddStudent(Student student); // [cite: 206]
    List<Student> GetAllStudents(); // [cite: 206]
    Student GetStudentById(int id); // [cite: 206]
    void DeleteStudent(int id); // [cite: 206]
}

[cite_start]// Repository Implementation [cite: 207]
[cite_start]
public class StudentRepository : IStudentRepository // [cite: 208]
{
    private List<Student> _students = new List<Student>(); // Store data using List<Student> [cite: 209, 210]

    public void AddStudent(Student student) => _students.Add(student);

    public List<Student> GetAllStudents() => _students;

    public Student GetStudentById(int id) => _students.FirstOrDefault(s => s.StudentId == id);

    public void DeleteStudent(int id)
    {
        var student = GetStudentById(id);
        if (student != null) _students.Remove(student);
    }
}

[cite_start]// Main Program [cite: 211]
class Program
{
    static void Main()
    {
        IStudentRepository repo = new StudentRepository();

        [cite_start]// Adding students [cite: 213]
        repo.AddStudent(new Student { StudentId = 1, StudentName = "John Doe", Course = "C#" });
        repo.AddStudent(new Student { StudentId = 2, StudentName = "Jane Smith", Course = "Java" });

        [cite_start]// Viewing students [cite: 214]
        Console.WriteLine("All Students:");
        foreach (var s in repo.GetAllStudents())
            Console.WriteLine($"{s.StudentId}: {s.StudentName} ({s.Course})");

        [cite_start]// Finding student by ID [cite: 215]
        var student = repo.GetStudentById(1);
        Console.WriteLine($"\nFound: {student?.StudentName}");

        [cite_start]// Deleting student [cite: 216]
        repo.DeleteStudent(1);
        Console.WriteLine($"\nTotal students after deletion: {repo.GetAllStudents().Count}");
    }
}