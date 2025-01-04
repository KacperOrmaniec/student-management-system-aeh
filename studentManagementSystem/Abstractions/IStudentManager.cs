using System.Collections;
using studentManagementSystem.Entities;

namespace studentManagementSystem.Abstractions;

public interface IStudentManager
{
    void AddStudent(Student student);
    void RemoveStudent(string studentId);
    void UpdateStudent(string studentId, string? name, int? age, double? grade);
    List<Student> DisplayAllStudents();
    double CalculateAverageGrade();
}