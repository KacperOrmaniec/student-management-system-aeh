namespace studentManagementSystem.Entities;

public class Student(string name, int age, double grade, string studentId)
{
    public string Name { get; set; } = name;
    public int Age { get; set; } = age;
    public double Grade { get; set; } = grade;
    public string StudentId { get; set; } = studentId;
    
    public void SetName(string name)
        => Name = name;
    
    public void SetAge(int age) 
        => Age = age;
    
    public void SetGrade(double grade) 
        => Grade = grade;
    
    public void SetStudentId(string studentId) 
        => StudentId = studentId;
}