using Microsoft.EntityFrameworkCore;
using studentManagementSystem.Entities;

namespace studentManagementSystem.Database;

public class StudentDbContext : DbContext
{
    public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    
}