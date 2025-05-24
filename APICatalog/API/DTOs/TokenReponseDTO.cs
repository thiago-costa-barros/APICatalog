using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.API.DTOs
{
    public class TokenReponseDTO
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int UserId { get; set; }
    }
}
