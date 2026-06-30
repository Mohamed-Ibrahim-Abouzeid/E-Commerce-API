using E_Commerce.Application.Common;
using E_Commerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Specifications
{
    public class ProductWithBranAndTypeSpecifications : BaseSpecification<Product, int>

    {
        public ProductWithBranAndTypeSpecifications(ProductQueryParams queryParams)
      : base(p =>
          (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value) &&
          (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value) &&
          (string.IsNullOrEmpty(queryParams.SearchValue) ||
           p.ProductName.ToLower().Contains(queryParams.SearchValue.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryParams.Sort)
            {
                case ProductSortingOptions.NameAsc:
                    AddOrderBy(p => p.ProductName);
                    break;

                case ProductSortingOptions.NameDesc:
                    AddOrderByDesc(p => p.ProductName);
                    break;

                case ProductSortingOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;

                case ProductSortingOptions.PriceDesc:
                    AddOrderByDesc(p => p.Price);
                    break;

                default:
                    AddOrderBy(p => p.Id);
                    break;
            }
            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        public ProductWithBranAndTypeSpecifications(int id)
            : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}