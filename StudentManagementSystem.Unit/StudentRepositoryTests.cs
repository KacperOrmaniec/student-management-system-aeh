
using Microsoft.EntityFrameworkCore;
using studentManagementSystem.Data.Database;
using studentManagementSystem.Data.Types;

namespace StudentManagementSystem.Unit;

public class StudentRepositoryTests
{
    private static StudentDbContext GetInMemoryDbContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
        return new StudentDbContext(options);
    }

    [Fact]
    public void Add_ShouldAddStudentToDatabase()
    {
        var context = GetInMemoryDbContext("Test_add");
        var repository = new StudentRepository(context);
        var student = new Student("John Doe", 20, 4.0, "123");
        
        repository.Add(student);
        
        context.Students.ShouldContain(student);
    }

    [Fact]
    public void GetById_ShouldReturnCorrectStudent()
    {
        var context = GetInMemoryDbContext("Test_getById");
        var repository = new StudentRepository(context);
        var student = new Student("John Doe", 20, 4.0, "124");
        context.Students.Add(student);
        context.SaveChanges();
        
        var result = repository.GetById("124");
        
        result.ShouldNotBeNull();
        result.Name.ShouldBe("John Doe");
        result.Age.ShouldBe(20);
        result.Grade.ShouldBe(4.0);
        result.StudentId.ShouldBe("124");
    }
    
    [Fact]
    public void Remove_ShouldRemoveStudentFromDatabase()
    {
        var context = GetInMemoryDbContext("Test_remove");
        var repository = new StudentRepository(context);
        var student = new Student("John Doe", 20, 4.0, "125");
        context.Students.Add(student);
        context.SaveChanges();
        
        repository.Remove(student);
        
        context.Students.ShouldNotContain(student);
    }
    
    [Fact]
    public void GetAll_ShouldReturnAllStudents()
    {
        var context = GetInMemoryDbContext("Test_getAll");
        var repository = new StudentRepository(context);
        var student1 = new Student("John Doe", 20, 4.0, "126");
        var student2 = new Student("Jane Doe", 22, 3.5, "127");
        context.Students.AddRange(student1, student2);
        context.SaveChanges();

        var students = repository.GetAll();

        students.Count.ShouldBeGreaterThanOrEqualTo(2);
        students.ShouldContain(student1);
        students.ShouldContain(student2);
    }
    
    [Fact]
    public void CalculateAverageGrade_ShouldReturnCorrectAverage()
    {
        var context = GetInMemoryDbContext("Test_Average");
        var repository = new StudentRepository(context);
        var student1 = new Student("John Doe", 20, 4.0, "128");
        var student2 = new Student("Jane Doe", 22, 3.0, "129");
        context.Students.AddRange(student1, student2);
        context.SaveChanges();
        
        var averageGrade = repository.CalculateAverageGrade();
        
        averageGrade.ShouldBe(3.5);
    }

    [Fact]
    public void CalculateAverageGrade_ShouldReturnZero_WhenNoStudentsExist()
    {
        var context = GetInMemoryDbContext("Test_calculateWithNoStudent");
        var repository = new StudentRepository(context);
        
        var averageGrade = repository.CalculateAverageGrade();
        
        averageGrade.ShouldBe(0);
    }

}