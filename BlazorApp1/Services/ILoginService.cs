using BreadRecipesWithWasmBlazor.Client.ViewModels;

namespace BreadRecipesWithWasmBlazor.Client.Services
{
    public interface ILoginService
    {
        Task<bool> Login(LoginUserModel loginModel);
    }
}