using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Auth;

public class AuthorizationPolicies
{
	public static void AddPolicies(IServiceCollection services)
	{
		services.AddAuthorizationCore(options =>
		{
			options.AddPolicy("MustBeVia", a =>
				a.RequireAuthenticatedUser().RequireClaim("Domain", "via"));

			options.AddPolicy("SecurityLevel4", a =>
				a.RequireAuthenticatedUser().RequireClaim("SecurityLevel", "4", "5"));

			options.AddPolicy("MustBeTeacher", a =>
				a.RequireAuthenticatedUser().RequireClaim("Role", "Teacher"));
		});
	}
}
