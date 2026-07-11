using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    
    public class BasketController(IBasketService basketService) : APIBaseController
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BasketDto),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken ct)
        {
            var basket = await basketService.GetBasketAsync(id, ct);
            return ToActionResult(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket, CancellationToken ct)
        {
            var saved = await basketService.CreateOrUpdateBasketAsync(basket, ct);
            return ToActionResult(saved);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken ct)
        {
            var deleted = await basketService.DeleteBasketAsync(id, ct);
            return ToActionResult(deleted);
        }
    }
}
