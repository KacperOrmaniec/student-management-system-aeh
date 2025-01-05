using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using Microsoft.Extensions.DependencyInjection;
using studentManagementSystem.Core.Abstractions;
using studentManagementSystem.Data.Types;

namespace studentManagementSystem.Gui;

public partial class MainWindow : Window
{
    private readonly IStudentManager _studentManager;

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _studentManager = serviceProvider.GetRequiredService<IStudentManager>();
    }

    private void OnAddStudentClicked(object? sender, RoutedEventArgs e)
    {
        try
        {
            var studentId = ValidateString(StudentIdInput.Text, "Student ID");
            var name = ValidateString(NameInput.Text, "Name");
            var age = ValidateInt(AgeInput.Text, "Age", 1, 100);
            var grade = ValidateDouble(GradeInput.Text, "Grade", 1.00, 6.00);
            
            var student = new Student(name, age, grade, studentId);
            _studentManager.AddStudent(student);
            
            OutputArea.Text += "Student added successfully.\n";
        }
        catch (Exception ex)
        {
            OutputArea.Text += $"Error: {ex.Message}\n";
        }
    }

    private void OnRemoveStudentClicked(object? sender, RoutedEventArgs e)
    {
        try
        {
            var studentId = ValidateString(StudentIdInput.Text, "Student ID");
            _studentManager.RemoveStudent(studentId);
            
            OutputArea.Text += "Student removed successfully.\n";
        }
        catch (Exception ex)
        {
            OutputArea.Text += $"Error: {ex.Message}\n";
        }
    }

    private void OnUpdateStudentClicked(object? sender, RoutedEventArgs e)
    {
        try
        {
            var studentId = ValidateString(StudentIdInput.Text, "Student ID");
            var name = ValidateOptionalString(NameInput.Text);
            var age = ValidateOptionalInt(AgeInput.Text, "Age", 1, 100);
            var grade = ValidateOptionalDouble(GradeInput.Text, "Grade", 1.00, 6.00);

            _studentManager.UpdateStudent(studentId, name, age, grade);
            
            OutputArea.Text += "Student updated successfully.\n";
        }
        catch (Exception ex)
        {
            OutputArea.Text += $"Error: {ex.Message}\n";
        }
    }

    private void OnDisplayAllStudentsClicked(object? sender, RoutedEventArgs e)
    {
        try
        {
            var students = _studentManager.DisplayAllStudents();
            OutputArea.Text += "All Students:\n";
            foreach (var student in students)
            {
                OutputArea.Text += $"{student.Name} - {student.StudentId} - Age: {student.Age}, Grade: {student.Grade}\n";
            }
        }
        catch (Exception ex)
        {
            OutputArea.Text += $"Error: {ex.Message}\n";
        }
    }

    private void OnCalculateAverageClicked(object? sender, RoutedEventArgs e)
    {
        try
        {
            var average = _studentManager.CalculateAverageGrade();
            OutputArea.Text += $"Average Grade: {average:F2}\n";
        }
        catch (Exception ex)
        {
            OutputArea.Text += $"Error: {ex.Message}\n";
        }
    }
    
    private static string ValidateString(string? input, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentException($"{fieldName} cannot be empty.");
        }
        return input.Trim();
    }

    private static int ValidateInt(string? input, string fieldName, int min, int max)
    {
        if (!int.TryParse(input, out var value))
        {
            throw new ArgumentException($"{fieldName} must be a valid integer.");
        }
        if (value < min || value > max)
        {
            throw new ArgumentException($"{fieldName} must be between {min} and {max}.");
        }
        return value;
    }

    private static double ValidateDouble(string? input, string fieldName, double min, double max)
    {
        if (!double.TryParse(input, out var value))
        {
            throw new ArgumentException($"{fieldName} must be a valid number.");
        }
        if (value < min || value > max)
        {
            throw new ArgumentException($"{fieldName} must be between {min:F2} and {max:F2}.");
        }
        return value;
    }

    private string? ValidateOptionalString(string? input)
    {
        return string.IsNullOrWhiteSpace(input) ? null : input.Trim();
    }

    private static int? ValidateOptionalInt(string? input, string fieldName, int min, int max)
    {
        if (string.IsNullOrWhiteSpace(input)) return null;
        return ValidateInt(input, fieldName, min, max);
    }

    private static double? ValidateOptionalDouble(string? input, string fieldName, double min, double max)
    {
        if (string.IsNullOrWhiteSpace(input)) return null;
        return ValidateDouble(input, fieldName, min, max);
    }
}
