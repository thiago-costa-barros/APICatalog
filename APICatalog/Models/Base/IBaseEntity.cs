namespace APICatalog.Models.Base
{
    public interface IBaseEntity
    {
        DateTime CreationDate { get; set; }
        DateTime UpdateDate { get; set; }
        DateTime? DeletionDate { get; set; }
        int? CreationUserId { get; set; }
        int? UpdateUserId { get; set; }
    }
}
