using studentManagementSystem.Data.Types;

namespace studentManagementSystem.Data.Abstractions;

public interface IStudentRepository
{
    void Add(Student student);
    void Remove(Student student);
    Student? GetById(string studentId);
    List<Student> GetAll();
    double CalculateAverageGrade();
}