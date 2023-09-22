using BreadRecipesWithWasmBlazor.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BreadRecipesWithWasmBlazor.Client.Services
{
	public class LoginService
	{
		public static Task<bool> Login(LoginUserModel loginModel)
		{
			if (loginModel.Username == "admin" && loginModel.Password == "password")
			{
				Task.FromResult<bool>(true);
			}
			return Task.FromResult<bool>(false);
		}
	}
}
