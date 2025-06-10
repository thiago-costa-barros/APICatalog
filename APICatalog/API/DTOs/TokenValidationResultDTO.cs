using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.API.DTOs
{
    public class TokenValidationResultDTO
    {
        public TokenStatus TokenStatus { get; set; }
        public int? Userid { get; set; }
        public string? Token { get; set; }
        public TokenType TokenType { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}
