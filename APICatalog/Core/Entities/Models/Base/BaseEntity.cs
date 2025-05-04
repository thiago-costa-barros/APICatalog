using System.Text.Json.Serialization;

namespace APICatalog.APICatalog.Core.Entities.Models.Base
{
    public class BaseEntity : IBaseEntity
    {
        [JsonIgnore]
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;
        [JsonIgnore]
        public DateTime? DeletionDate { get; set; }
        [JsonIgnore]
        public int? CreationUserId { get; set; }
        [JsonIgnore]
        public int? UpdateUserId { get; set; }
    }
}
