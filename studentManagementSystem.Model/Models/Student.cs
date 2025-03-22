namespace studentManagementSystem.Model.Models;

public class Student(string name, int age, double grade, string studentId)
{
    public string StudentId { get; set; } = studentId;

    public string Name { get; set; } = name;
    public int Age { get; set; } = age;
    public double Grade { get; set; } = grade;
}