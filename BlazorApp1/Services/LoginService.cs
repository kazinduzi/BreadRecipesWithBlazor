using BreadRecipesWithWasmBlazor.Client.ViewModels;

namespace BreadRecipesWithWasmBlazor.Client.Services
{
    public class LoginService : ILoginService
    {
        public Task<bool> Login(LoginUserModel loginModel)
        {
            if (loginModel.Username == "admin" && loginModel.Password == "password")
            {
                Task.FromResult<bool>(true);
            }
            return Task.FromResult<bool>(false);
        }
    }
}
