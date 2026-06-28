using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
   
    public class ProductsController(IProductService service) : APIBaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts(CancellationToken ct) {
            var products = await service.GetAllProductsAsync(ct);
            return ToActionResult(products);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id,CancellationToken ct)
        {
            var products = await service.GetProductAsync(id,ct);
            return ToActionResult(products);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
            =>ToActionResult (await service.GetAllBrandsAsync(ct));
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
    => ToActionResult(await service.GetAllTypesAsync(ct));
    }
}
