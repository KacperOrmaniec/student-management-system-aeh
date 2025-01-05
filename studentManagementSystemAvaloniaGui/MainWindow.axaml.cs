using Avalonia.Controls;
using Avalonia.Interactivity;
using studentManagementSystem.Entities;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using studentManagementSystem.Abstractions;
using studentManagementSystem.Database;

namespace studentManagementSystemAvaloniaGui;

public partial class MainWindow : Window
{
    private readonly IStudentManager _studentManager;

    public MainWindow(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        // Initialize the StudentManager (Replace with Dependency Injection if applicable)
        _studentManager = serviceProvider.GetRequiredService<IStudentManager>();
    }

    private void OnAddStudentClicked(object? sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(StudentIdInput.Text))
            {
                OutputArea.Text += "Error: Student ID cannot be empty.\n";
                return;
            }
            
            if (string.IsNullOrWhiteSpace(NameInput.Text))
            {
                OutputArea.Text += "Error: Name cannot be empty.\n";
                return;
            }
            
            if (string.IsNullOrWhiteSpace(AgeInput.Text))
            {
                OutputArea.Text += "Error: Age cannot be empty.\n";
                return;
            }
            
            if (string.IsNullOrWhiteSpace(GradeInput.Text))
            {
                OutputArea.Text += "Error: Grade cannot be empty.\n";
                return;
            }

            var student = new Student(NameInput.Text, 
                                      int.Parse(AgeInput.Text), 
                                      double.Parse(GradeInput.Text), 
                                      StudentIdInput.Text);
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
            if (string.IsNullOrWhiteSpace(StudentIdInput.Text))
            {
                OutputArea.Text += "Error: Student ID cannot be empty.\n";
                return;
            }
            
            _studentManager.RemoveStudent(StudentIdInput.Text);
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
            if (string.IsNullOrWhiteSpace(StudentIdInput.Text))
            {
                OutputArea.Text += "Error: Student ID cannot be empty.\n";
                return;
            }
            
            _studentManager.UpdateStudent(StudentIdInput.Text, 
                                          NameInput.Text, 
                                          int.TryParse(AgeInput.Text, out var age) ? age : (int?)null, 
                                          double.TryParse(GradeInput.Text, out var grade) ? grade : (double?)null);
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
}
