// studentManagementSystem.Web/Controllers/StudentController.cs
using Microsoft.AspNetCore.Mvc;
using studentManagementSystem.Data.Abstractions;
using studentManagementSystem.Model.Models;

namespace studentManagementSystem.Web.Controllers;

public class StudentController(IStudentManager studentManager) : Controller
{
    // GET: Student/All
    public IActionResult All()
    {
        var students = studentManager.DisplayAllStudents();
        return View(students); // Widok będzie obsługiwany przez inną osobę
    }

    // POST: Student/Add
    [HttpPost]
    public IActionResult Add(Student student)
    {
        if (ModelState.IsValid)
        {
            studentManager.AddStudent(student);
            return RedirectToAction("All");
        }
        return View(student);
    }

    // Analogicznie dla innych akcji: Remove, Update, CalculateAverageGrade
}