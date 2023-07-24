using Microsoft.EntityFrameworkCore;
using CustomMiddlewareWithAPI.Entities;

namespace CustomMiddlewareWithAPI.Data;

public class StudentContext : DbContext
{
	public StudentContext(DbContextOptions<StudentContext> options)
		: base(options) { }

	public DbSet<Student> Students { get; set; }
}
