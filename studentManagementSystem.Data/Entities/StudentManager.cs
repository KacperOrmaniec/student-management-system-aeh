using studentManagementSystem.Model.Models;
using studentManagementSystem.Data.Abstractions;

namespace studentManagementSystem.Data.Entities;

public class StudentManager(IStudentRepository repository) : IStudentManager
{
    private readonly IStudentRepository _repository = repository;

    public void AddStudent(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name))
            throw new ArgumentException("Student name cannot be empty.");

        if (student.Age is < 1 or > 100)
            throw new ArgumentException("Student age must be between 1 and 100.");

        if (student.Grade is < 1.00 or > 6.00)
            throw new ArgumentException("Student grade must be between 1.00 and 6.00.");

        _repository.Add(student);
    }

    public void RemoveStudent(string studentId)
    {
        var student = _repository.GetById(studentId)
                      ?? throw new InvalidOperationException("Student not found.");
        _repository.Remove(student);
    }

    public void UpdateStudent(string studentId, string? name = null, int? age = null, double? grade = null)
    {
        var student = _repository.GetById(studentId)
                      ?? throw new InvalidOperationException("Student not found.");

        if (!string.IsNullOrWhiteSpace(name)) student.Name = name;
        if (age is > 0 and <= 100) student.Age = age.Value;
        if (grade is >= 1.00 and <= 6.00) student.Grade = grade.Value;

        _repository.Update(student);
    }

    public List<Student> DisplayAllStudents() => _repository.GetAll();

    public double CalculateAverageGrade() => _repository.CalculateAverageGrade();
}