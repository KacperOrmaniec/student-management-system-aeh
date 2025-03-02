using studentManagementSystem.Model.Models;

namespace studentManagementSystem.Data.Abstractions;

public interface IStudentRepository
{
    void Add(Student student);
    void Remove(Student student);
    void Update(Student student);
    Student? GetById(string studentId);
    List<Student> GetAll();
    double CalculateAverageGrade();
}