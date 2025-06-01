using APICatalog.APICatalog.Core.Entities.Models;
using APICatalog.APICatalog.Core.Entities.Models.Base;
using static APICatalog.Core.Common.Enum.PublicEnum;

namespace APICatalog.Core.Entities.Models
{
    public class UserToken: AuditableEntity
    {
        public int UserTokenId { get; set; }
        public int UserId { get; set; }
        public string JwtToken { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public TokenType Type { get; set; }
        public TokenStatus Status { get; set; }
        public Guid Identifier { get; set; }

        public User? User { get; set; } = null!;
    }
}
