using CustomMiddlewareWithAPI.Data;
using Microsoft.EntityFrameworkCore;
using CustomMiddlewareWithAPI.Entities;
using CustomMiddlewareWithAPI.Repositories.Abstract;

namespace CustomMiddlewareWithAPI.Repositories.Concrete;

public class StudentRepository : IStudentRepository
{
	private readonly StudentContext _context;

	public StudentRepository(StudentContext context)
	{
		_context = context;
	}

	public async void Add(Student entity)
	{
		await _context.Students.AddAsync(entity);
		await _context.SaveChangesAsync();
	}

	public async void Delete(Student entity)
	{
		_context.Students.Remove(entity);
		await _context.SaveChangesAsync();
	}

	public IEnumerable<Student> GetAll()
	{
		return _context.Students;
	}

	public Student GetByID(int id)
	{
		return _context.Students.FirstOrDefault(s => s.Id == id);
	}

	public async void Update(Student entity)
	{
		_context.Entry(entity).State = EntityState.Modified;
		await _context.SaveChangesAsync();
	}
}
