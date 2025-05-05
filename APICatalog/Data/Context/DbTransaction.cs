using APICatalog.APICatalog.Data.Repositories.Categories;
using APICatalog.APICataolog.Data.Context;
using APICatalog.Data.Repositories.Products;
using Microsoft.EntityFrameworkCore.Storage;

namespace APICatalog.Data.Context
{
    public class DbTransaction : IDbTransaction
    {
        private IProductRepository? _productRepo;
        private ICategoryRepository? _categoryRepo;
        private IDbContextTransaction? _transaction;

        private AppDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        public DbTransaction(AppDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }
        public IProductRepository ProductRepository => _productRepo ??= _serviceProvider.GetRequiredService<IProductRepository>();
        public ICategoryRepository CategoryRepository => _categoryRepo ??= _serviceProvider.GetRequiredService<ICategoryRepository>();
        
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                _transaction?.Rollback();
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
