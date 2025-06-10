using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.API.DTOs
{
    public class TokenReponseDTO
    {
        public int UserId { get; set; }
        public AccessTokenResponseDTO? AccessToken { get; set; }
        public RefreshTokenResponseDTO? RefreshToken { get; set; }
        public DateTime RequestTime { get; set; } //apenas para controle de requisição
    }

    public class AccessTokenResponseDTO
    {
        public Guid AccessIdentifier { get; set; }
        public string? AccessToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
    public class RefreshTokenResponseDTO
    {
        public Guid RefreshIdentifier { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
