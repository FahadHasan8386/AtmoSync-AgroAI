
namespace AtmoSync.Shared.Models.DtoModels
{
    public class RefreshTokenRequestDto
    {
        public long UserId {  get; set; }
        public required string RefreshToken { get; set; }   

    }
}
