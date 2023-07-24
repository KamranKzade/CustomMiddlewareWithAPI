using System.Text;
using System.Security.Claims;


namespace CustomMiddlewareWithAPI.Middlewares;


public class AuthenticationMiddware
{
	private readonly RequestDelegate _next;

	public AuthenticationMiddware(RequestDelegate next)
	{
		_next = next;
	}


	public async Task Invoke(HttpContext context)
	{
		string authHeader = context.Request.Headers["Authorization"];

		if (authHeader == null)
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			return;
		}

		if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
		{
			var token = authHeader.Substring(6).Trim();
			string credentialString = "";

			try
			{
				credentialString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = StatusCodes.Status500InternalServerError;
				return;
			}

			var creadential = credentialString.Split(':');
			var username = creadential[0];
			var password = creadential[1];

			// For testing
			if (username == "kamran1999" && password == "12345")
			{
				var claim = new[]
				{
					new Claim("username", username),
					new Claim(ClaimTypes.Role, "Admin")
				};

				var identity = new ClaimsIdentity(claim,"Basic");
				context.User = new ClaimsPrincipal(identity);
				await _next(context);
			}
			else
			{
				context.Response.StatusCode = StatusCodes.Status401Unauthorized;
				return;
			}
		}
	}
}