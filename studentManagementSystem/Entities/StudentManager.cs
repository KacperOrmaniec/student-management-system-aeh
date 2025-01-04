using studentManagementSystem.Abstractions;
using studentManagementSystem.Database;

namespace studentManagementSystem.Entities;

public class StudentManager(StudentDbContext studentDbContext) : IStudentManager
{
    private readonly StudentDbContext _studentDbContext = studentDbContext;
    public void AddStudent(Student student)
    {
        _studentDbContext.Students.Add(student);
        _studentDbContext.SaveChanges();
    }

    public void RemoveStudent(string studentId)
    {
        var student = FindStudent(studentId);
        
        _studentDbContext.Students.Remove(student);
        _studentDbContext.SaveChanges();
    }

    public void UpdateStudent(string studentId, string? name = null, int? age = null, double? grade = null)
    {
        var student = FindStudent(studentId);
        
        if (name != null) student.Name = name;
        if (age.HasValue) student.Age = age.Value;
        if (grade.HasValue) student.Grade = grade.Value;
        
        _studentDbContext.SaveChanges();
    }

    public List<Student> DisplayAllStudents()
        => _studentDbContext.Students.ToList();

    public double CalculateAverageGrade()
    {
        if (!_studentDbContext.Students.Any()) return 0;
        return _studentDbContext.Students.Average(s => s.Grade);
    }

    private Student FindStudent(string studentId)
    {
        var student = _studentDbContext.Students.Find(studentId);
        if (student == null)
        {
            throw new InvalidOperationException($"Student with ID {studentId} not found.");
        }

        return student;
    }
}