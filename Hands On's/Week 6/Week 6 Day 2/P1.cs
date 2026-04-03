using System;
using System.Collections.Generic;

[cite_start]// 1. Student class with properties [cite: 6]
public class Student
{
    public int StudentId { get; set; } // [cite: 7]
    public string StudentName { get; set; } // [cite: 8]
    public double Marks { get; set; } // [cite: 9]
}

[cite_start]// 2. Class responsible for managing student data [cite: 10, 21]
public class StudentRepository
{
    private List<Student> _students = new List<Student>();

    public void AddStudent(Student student)
    {
        _students.Add(student);
    }

    public List<Student> GetAllStudents()
    {
        return _students;
    }
}

[cite_start]// 3. Separate class responsible for generating reports [cite: 11, 22]
public class ReportGenerator
{
    [cite_start]
    public void PrintReport(List<Student> students) // [cite: 23]
    {
        Console.WriteLine("--- Student Performance Report ---");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.StudentId}, Name: {student.StudentName}, Marks: {student.Marks}");
        }
    }
}

class Program
{
    static void Main()
    {
        var repo = new StudentRepository();
        repo.AddStudent(new Student { StudentId = 1, StudentName = "Alice", Marks = 85.5 });
        repo.AddStudent(new Student { StudentId = 2, StudentName = "Bob", Marks = 90.0 });

        var reportGen = new ReportGenerator();
        reportGen.PrintReport(repo.GetAllStudents());
    }
}
