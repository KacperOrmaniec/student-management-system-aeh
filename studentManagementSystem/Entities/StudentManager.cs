using Microsoft.EntityFrameworkCore;
using studentManagementSystem.Abstractions;
using studentManagementSystem.Database;

namespace studentManagementSystem.Entities;

public class StudentManager(StudentDbContext studentDbContext) : IStudentManager
{
    private readonly StudentDbContext _studentDbContext = studentDbContext;
    public void AddStudent(Student student)
    {
        try
        {
            _studentDbContext.Students.Add(student);
            _studentDbContext.SaveChanges();
        }
        catch (DbUpdateException dbEx)
        {
            throw new InvalidOperationException("A database error occurred while adding the student.", dbEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while adding the student.", ex);
        }
    }

    public void RemoveStudent(string studentId)
    {
        try
        {
            var student = FindStudent(studentId);
            
            _studentDbContext.Students.Remove(student);
            _studentDbContext.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Failed to remove student: {ex.Message}", ex);
        }
        catch (DbUpdateException dbEx)
        {
            throw new InvalidOperationException("A database error occurred while removing the student.", dbEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while removing the student.", ex);
        }
    }

    public void UpdateStudent(string studentId, string? name = null, int? age = null, double? grade = null)
    {
        try
        {
            var student = FindStudent(studentId);
            
            if (name != null) student.Name = name;
            if (age.HasValue) student.Age = age.Value;
            if (grade.HasValue) student.Grade = grade.Value;
            
            _studentDbContext.SaveChanges();
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Failed to update student: {ex.Message}", ex);
        }
        catch (DbUpdateException dbEx)
        {
            throw new InvalidOperationException("A database error occurred while updating the student.", dbEx);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while updating the student.", ex);
        }
    }

    public List<Student> DisplayAllStudents()
    {
        try
        {
            return _studentDbContext.Students.ToList();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while retrieving the list of students.", ex);
        }
    }

    public double CalculateAverageGrade()
    {
        try
        {
            if (!_studentDbContext.Students.Any())
                return 0;

            return _studentDbContext.Students.Average(s => s.Grade);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An error occurred while calculating the average grade.", ex);
        }
    }

    private Student FindStudent(string studentId)
    {
        try
        {
            var student = _studentDbContext.Students.Find(studentId);
            if (student == null)
            {
                throw new InvalidOperationException($"Student with ID {studentId} not found.");
            }

            return student;
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Failed to find student: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while finding the student.", ex);
        }
    }
}