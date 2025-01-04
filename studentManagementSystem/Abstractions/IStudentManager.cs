using System.Collections;
using studentManagementSystem.Entities;

namespace studentManagementSystem.Abstractions;

public interface IStudentManager
{
    void AddStudent(Student student);
    void RemoveStudent(string studentId);
    void UpdateStudent(string studentId);
    List<Student> DisplayAllStudents();
    double CalculateAverageGrade();
}