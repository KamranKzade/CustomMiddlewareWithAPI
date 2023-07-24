using System.Text;
using CustomMiddlewareWithAPI.Dtos;
using Microsoft.AspNetCore.Mvc;
using CustomMiddlewareWithAPI.Services.Abstract;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomMiddlewareWithAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IStudentService _studentService;

	public AccountController(IStudentService studentService)
	{
		_studentService = studentService;
	}

	[HttpPost("SignIn")]
	public IActionResult SignIn(SignInDto dto)
	{
		var student = _studentService.GetAll()
									 .FirstOrDefault(s => s.Username == dto.Username && s.Password == dto.Password);

		if (student != null)
		{
			var token = student.Username + ":" + student.Password;
			return Ok(Convert.ToBase64String(Encoding.UTF8.GetBytes(token)));

		}
		return Unauthorized();
	}


}
