using APICatalog.APICatalog.Data.Repositories.Categories;
using APICatalog.Data.Repositories.Products;


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
