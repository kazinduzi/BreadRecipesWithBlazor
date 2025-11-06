using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BreadRecipesWithWasmBlazor.Client.AuthProviders
{
	public class TestAuthStateProvider : AuthenticationStateProvider
	{
		public async override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			await Task.Delay(1500);
			
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, "Joh Doe"),
				new Claim(ClaimTypes.Role, "Administrator")
			};
			var anonymous = new ClaimsIdentity(claims, "testAuthType");

			var authState = new AuthenticationState(new ClaimsPrincipal(anonymous));
			return await Task.FromResult(authState);
		}
	}
}
