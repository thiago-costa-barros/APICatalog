﻿namespace APICatalog.APICatalog.Core.Entities.Models.Base
{
    public interface IAuditableEntity
    {
        DateTime CreationDate { get; set; }
        DateTime UpdateDate { get; set; }
        DateTime? DeletionDate { get; set; }
        int? CreationUserId { get; set; }
        int? UpdateUserId { get; set; }
    }
}
