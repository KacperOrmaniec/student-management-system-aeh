using Microsoft.AspNetCore.Mvc;
using studentManagementSystem.Data.Abstractions;
using studentManagementSystem.Model.Models;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace studentManagementSystem.Web.Controllers;

[ApiController]
[Route("api/students")] 
[Produces("application/json")]
[SwaggerTag("Manage student records and grades")]
public class StudentController : ControllerBase
{
    private readonly IStudentManager _studentManager;

    public StudentController(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    /// <summary>
    /// Get all students
    /// </summary>
    [HttpGet(Name = "GetAllStudents")]
    [SwaggerOperation(Summary = "Retrieve all students")]
    [SwaggerResponse(200, "List of students", typeof(List<Student>))]
    public IActionResult GetAllStudents()
    {
        var students = _studentManager.DisplayAllStudents();
        return Ok(students);
    }

    /// <summary>
    /// Get student by ID
    /// </summary>
    /// <param name="studentId">Student identification number</param>
    [HttpGet("{studentId}", Name = "GetStudentById")]
    [SwaggerOperation(Summary = "Retrieve a specific student")]
    [SwaggerResponse(200, "Student found", typeof(Student))]
    [SwaggerResponse(404, "Student not found")]
    public IActionResult GetStudentById(
        [SwaggerParameter("Student ID", Required = true)]
        string studentId)
    {
        try
        {
            var student = _studentManager.DisplayAllStudents()
                .FirstOrDefault(s => s.StudentId == studentId);

            return student != null ? Ok(student) : NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Add new student
    /// </summary>
    [HttpPost(Name = "CreateStudent")]
    [SwaggerOperation(Summary = "Create a new student record")]
    [SwaggerResponse(201, "Student created", typeof(Student))]
    [SwaggerResponse(400, "Invalid input")]
    [SwaggerResponse(409, "Student already exists")]
    public IActionResult AddStudent(
        [FromBody, SwaggerRequestBody("Student details")] Student student)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _studentManager.AddStudent(student);
            return CreatedAtRoute("GetStudentById",
                new { studentId = student.StudentId }, student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Update student details
    /// </summary>
    /// <param name="studentId">Student identification number</param>
    [HttpPut("{studentId}", Name = "UpdateStudent")]
    [SwaggerOperation(Summary = "Update existing student information")]
    [SwaggerResponse(204, "Student updated")]
    [SwaggerResponse(400, "Invalid input")]
    [SwaggerResponse(404, "Student not found")]
    public IActionResult UpdateStudent(
        [SwaggerParameter("Student ID", Required = true)] string studentId,
        [FromBody, SwaggerRequestBody("Updated student data")] StudentUpdateDto updateDto)
    {
        try
        {
            _studentManager.UpdateStudent(
                studentId,
                updateDto.Name,
                updateDto.Age,
                updateDto.Grade
            );

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Delete student record
    /// </summary>
    /// <param name="studentId">Student identification number</param>
    [HttpDelete("{studentId}", Name = "DeleteStudent")]
    [SwaggerOperation(Summary = "Remove a student record")]
    [SwaggerResponse(204, "Student deleted")]
    [SwaggerResponse(404, "Student not found")]
    public IActionResult DeleteStudent(
        [SwaggerParameter("Student ID", Required = true)] string studentId)
    {
        try
        {
            _studentManager.RemoveStudent(studentId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
    }

    /// <summary>
    /// Get average grade
    /// </summary>
    [HttpGet("average", Name = "GetAverageGrade")]
    [SwaggerOperation(Summary = "Calculate average grade of all students")]
    [SwaggerResponse(200, "Average grade calculated", typeof(double))]
    [SwaggerResponse(400, "No students found")]
    public IActionResult GetAverageGrade()
    {
        try
        {
            var average = _studentManager.CalculateAverageGrade();
            return Ok(new { AverageGrade = average });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public class StudentUpdateDto
{
    [SwaggerSchema("Student name", Nullable = true)]
    public string? Name { get; set; }

    [Range(1, 100)]
    [SwaggerSchema("Student age", Nullable = true)]
    public int? Age { get; set; }

    [Range(1.0, 6.0)]
    [SwaggerSchema("Student grade", Nullable = true)]
    public double? Grade { get; set; }
}