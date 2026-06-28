using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey> {
          var query= inputQuery;
            if (specifications.Criteria != null) 
            {
                query = query.Where(specifications.Criteria);
            }

            if (specifications.IncludeExpressions.Any()) {
                query = specifications.IncludeExpressions.Aggregate(query, (current, NextExp) => current.Include(NextExp));
            }
            return query;
            
        }
    }
}
