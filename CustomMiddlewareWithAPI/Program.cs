using CustomMiddlewareWithAPI.Data;
using CustomMiddlewareWithAPI.Middlewares;
using CustomMiddlewareWithAPI.Repositories.Abstract;
using CustomMiddlewareWithAPI.Repositories.Concrete;
using CustomMiddlewareWithAPI.Services.Abstract;
using CustomMiddlewareWithAPI.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();


var conn = builder.Configuration.GetConnectionString("myConn");
builder.Services.AddDbContext<StudentContext>(opt =>
{
	opt.UseSqlServer(conn);
});


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
				options => builder.Configuration.Bind("JwtSettings", options))
				.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
				options => builder.Configuration.Bind("CookieSettings", options));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<AuthenticationMiddware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();