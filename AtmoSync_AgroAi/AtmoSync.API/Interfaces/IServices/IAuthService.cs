using AtmoSync.Shared;
using AtmoSync.Shared.Models.DtoModels;

namespace AtmoSync.API.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<ResponseModel<string>> RegisterAsync(RegisterDto dto);
        Task<ResponseModel<LoginResponseDto>> LoginAsync(LoginDto dto);
        Task<ResponseModel<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto dto);
        Task<ResponseModel<string>> LogoutAsync(long userId);
    }
}
