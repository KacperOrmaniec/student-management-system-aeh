using System.Collections;

namespace studentManagementSystem.Abstractions;

public interface IStudentManager
{
    void addStudent(Student student);
    void removeStudent(string studentID);
    void updateStudent(string studentID);
    ArrayList<Student> displayAllStudents();
    double calculateAverageGrade();
}