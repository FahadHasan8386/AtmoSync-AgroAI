
namespace AtmoSync.Shared.Models.DtoModels
{
    public class RefreshTokenRequestDto
    {
        public Guid UserId {  get; set; }
        public required string RefreshToken { get; set; }   

    }
}
