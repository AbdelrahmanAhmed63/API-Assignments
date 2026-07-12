using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;

namespace E_Commerce.Infrastructure.Repository;

internal class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
{
    private readonly Dictionary<string, object> _repositories = [];

    public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
    {
        var typeName = typeof(TEntity).Name;
        if (_repositories.TryGetValue(typeName, out object? value))
            return (IGenericRepository<TEntity, TKey>)value;
        else
        {
            var repo = new GenericRepository<TEntity, TKey>(dbContext);
            _repositories[typeName] = repo;
            return repo;
        }
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct = default) => await dbContext.SaveChangesAsync(ct);
}