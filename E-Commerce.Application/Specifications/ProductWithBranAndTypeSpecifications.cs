using E_Commerce.Application.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class ProductWithBranAndTypeSpecifications:BaseSpecification<Product,int>

    {
        public ProductWithBranAndTypeSpecifications(ProductQueryParams queryParams) :base
            (p=>(!queryParams.BrandId.HasValue||p.BrandId== queryParams.BrandId.Value)&&(!queryParams.TypeId.HasValue||p.TypeId== queryParams.TypeId.Value)&&(string.IsNullOrEmpty(queryParams.SearchValue) || p.ProductName.ToLower().Contains(queryParams.SearchValue)))
            {
            AddInclude(p => p.ProductName);
            AddInclude(p => p.ProductType);

        }
        public ProductWithBranAndTypeSpecifications(int id) : base(x=>x.Id==id)
        {
            AddInclude(p => p.ProductName);
            AddInclude(p => p.ProductType);

        }
    }
}
