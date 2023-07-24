using Microsoft.AspNetCore.Mvc;
using CustomMiddlewareWithAPI.Data;
using CustomMiddlewareWithAPI.Dtos;
using Microsoft.EntityFrameworkCore;
using CustomMiddlewareWithAPI.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CustomMiddlewareWithAPI.Controllers;


[Route("api/[controller]")]
[ApiController]
public class StudentController : ControllerBase
{
	private readonly StudentContext _studentContext;

	public StudentController(StudentContext studentContext)
	{
		_studentContext = studentContext;
	}


	// GET: api/<StudentController>
	[Authorize]
	[HttpGet]
	public async Task<IEnumerable<StudentDto>> Get()
	{
		var students = await _studentContext.Students.ToListAsync();

		var dtos = students.Select(s =>
		{
			return new StudentDto
			{
				Fullname = s.Name + ' ' + s.Surname,
				Age = s.Age,
			};
		});
		return dtos;
	}

	// GET api/<StudentController>/5
	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var student = await _studentContext.Students.SingleOrDefaultAsync(s => s.Id == id);
		if (student != null)
		{

			var dtos = new StudentDto
			{
				Fullname = student.Name + " " + student.Surname,
				Age = student.Age,
			};

			return Ok(dtos);
		}
		return BadRequest();
	}

	// POST api/<StudentController>
	[HttpPost]
	public async Task<IActionResult> Post([FromBody] StudentAddDto dto)
	{
		try
		{
			var student = new Student
			{
				Name = dto.Name,
				Age = dto.Age,
				Surname = dto.Surname
			};
			await _studentContext.Students.AddAsync(student);
			await _studentContext.SaveChangesAsync();
			return Ok(student);
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	// PUT api/<StudentController>/5
	[HttpPut("{id}")]
	public async Task<IActionResult> Put(int id, [FromBody] StudentUpdateDto dto)
	{
		try
		{
			var item = await _studentContext.Students.SingleOrDefaultAsync(s => s.Id == id);
			if (item != null)
			{
				item.Name = dto.Name;
				item.Age = dto.Age;
				item.Surname = dto.Surname;

				_studentContext.Students.Update(item);
				await _studentContext.SaveChangesAsync();
				return Ok(item);
			}
			else return BadRequest();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}

	// DELETE api/<StudentController>/5
	[HttpDelete("DeleteProduct/{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			var item = await _studentContext.Students.SingleOrDefaultAsync(s => s.Id == id);
			if (item != null)
			{
				_studentContext.Students.Remove(item);
				await _studentContext.SaveChangesAsync();
				return NoContent();
			}
			else return BadRequest();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}

	}
}