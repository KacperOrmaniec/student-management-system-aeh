using studentManagementSystem.Data.Types;

namespace studentManagementSystem.Core.Abstractions;

public interface IStudentManager
{
    void AddStudent(Student student);
    void RemoveStudent(string studentId);
    void UpdateStudent(string studentId, string? name, int? age, double? grade);
    List<Student> DisplayAllStudents();
    double CalculateAverageGrade();
}