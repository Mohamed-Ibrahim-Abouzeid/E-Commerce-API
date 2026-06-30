using E_Commerce.Application.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class ProductCountSpecifications : BaseSpecification<Product, int>
    {
        public ProductCountSpecifications(ProductQueryParams queryParams)
      : base(p =>
          (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value) &&
          (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value) &&
          (string.IsNullOrEmpty(queryParams.SearchValue) ||
           p.ProductName.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
        }
    }
}
