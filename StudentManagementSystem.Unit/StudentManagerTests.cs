using studentManagementSystem.Data.Abstractions;
using studentManagementSystem.Core.Entities;
using studentManagementSystem.Data.Types;

namespace StudentManagementSystem.Unit;

public class StudentManagerTests
{
    #region Arrange

    private readonly Mock<IStudentRepository> _mockRepository;
    private readonly StudentManager _studentManager;

    public StudentManagerTests()
    {
        _mockRepository = new Mock<IStudentRepository>();
        
        _studentManager = new StudentManager(_mockRepository.Object);
    }

    #endregion
    

    [Fact]
    public void AddStudent_ShouldThrow_WhenNameIsEmpty()
    {
        var student = new Student("", 20, 4.0, "123");
        
        var exception = Should.Throw<ArgumentException>(() => _studentManager.AddStudent(student));
        
        exception.Message.ShouldBe("Student name cannot be empty.");
    }

    [Fact]
    public void AddStudent_ShouldThrow_WhenAgeIsOutOfRange()
    {
        var student = new Student("John Doe", 150, 4.0, "123");
        
        var exception = Should.Throw<ArgumentException>(() => _studentManager.AddStudent(student)); 
        
        exception.Message.ShouldBe("Student age must be between 1 and 100.");
    }

    [Fact]
    public void AddStudent_ShouldCallRepositoryAdd_WhenValidStudent()
    {
        var student = new Student("John Doe", 20, 4.0, "123");
        
        _studentManager.AddStudent(student);
        
        _mockRepository.Verify(r => r.Add(student), Times.Once);
    }

    [Fact]
    public void RemoveStudent_ShouldThrow_WhenStudentNotFound()
    {
        _mockRepository.Setup(r => r.GetById("123")).Returns((Student)null);
        
        var exception = Should.Throw<InvalidOperationException>(() => _studentManager.RemoveStudent("123"));
        
        exception.Message.ShouldBe("Student not found.");
    }

    [Fact]
    public void RemoveStudent_ShouldCallRepositoryRemove_WhenStudentExists()
    {
        var student = new Student("John Doe", 20, 4.0, "123");
        _mockRepository.Setup(r => r.GetById("123")).Returns(student);

        _studentManager.RemoveStudent("123");
        
        _mockRepository.Verify(r => r.Remove(student), Times.Once);
    }
}
