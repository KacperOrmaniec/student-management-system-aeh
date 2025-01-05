using studentManagementSystem.Data.Abstractions;
using studentManagementSystem.Data.Database;

namespace studentManagementSystem.Data.Types;

public class StudentRepository(StudentDbContext context) : IStudentRepository
{
    private readonly StudentDbContext _context = context;

    public void Add(Student student)
    {
        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void Remove(Student student)
    {
        _context.Students.Remove(student);
        _context.SaveChanges();
    }

    public Student? GetById(string studentId) =>
        _context.Students.FirstOrDefault(s => s.StudentId == studentId);

    public List<Student> GetAll() => _context.Students.ToList();

    public double CalculateAverageGrade() =>
        _context.Students.Any() ? _context.Students.Average(s => s.Grade) : 0;
}