using APICatalog.Data.Repositories.Interfaces;


namespace APICatalog.Data.Context
{
    public interface IDbTransaction
    {
        IProductRepository ProductRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        void Commit();
        void Rollback();
    }
}
