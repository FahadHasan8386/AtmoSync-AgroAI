using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<ResponseModel> RegisterAsync(RegisterDto dto);

        Task<ResponseModel> LoginAsync(LoginDto dto);

    }
}
