using CustomMiddlewareWithAPI.Entities;
using CustomMiddlewareWithAPI.Repositories.Abstract;
using CustomMiddlewareWithAPI.Services.Abstract;


namespace CustomMiddlewareWithAPI.Services.Concrete;


public class StudentService : IStudentService
{
	private readonly IStudentRepository _studentRepo;

	public StudentService(IStudentRepository studentRepo)
	{
		_studentRepo = studentRepo;
	}

	public void Add(Student entity) => _studentRepo.Add(entity);
	public void Update(Student entity) => _studentRepo.Update(entity);
	public void Delete(int id) => _studentRepo.Delete(_studentRepo.GetByID(id));

	public Student Get(int id)
	{
		return _studentRepo.GetByID(id);
	}

	public IEnumerable<Student> GetAll()
	{
		return _studentRepo.GetAll();
	}
}