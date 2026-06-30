
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Infrastructure.Repositories
{
    public class GenericRepositories<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
       public void Add(TEntity entity)
        => dbContext.Set<TEntity>().Add(entity);

        public Task<int> CountAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken ct = default)
        {
            return SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specifications).CountAsync(ct);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct)
        {
            return await dbContext.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specifications);
            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct)
        => await dbContext.Set<TEntity>().FindAsync([id!],ct).AsTask();

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), specifications);
            return await query.FirstOrDefaultAsync();
        }

        public void Remove(TEntity entity)
         => dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity)
         => dbContext.Set<TEntity>().Update(entity);
    }


}
