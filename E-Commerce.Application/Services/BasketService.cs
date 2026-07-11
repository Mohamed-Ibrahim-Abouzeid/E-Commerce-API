using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Baskets;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Services
{
    public class BasketService(IBasketRepository basketRepository, IMapper mapper) : IBasketService
    {
        public async Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket, CancellationToken ct = default)
        {
            var customerBasket = mapper.Map<CustomerBasket>(basket);
            var result = await basketRepository.CreateOrUpdateBasketAsync(customerBasket, ct:ct);
            return result != null ? Result<BasketDto>.Ok(mapper.Map<BasketDto>(result)) :
                Result<BasketDto>.Fail(Error.Failure("Failed to create or update the basket."));
        }

        public async Task<Result<bool>> DeleteBasketAsync(string Id, CancellationToken ct = default)
        {
var result = await basketRepository.DeleteBasketAsync(Id, ct: ct);
            return result ? Result<bool>.Ok(true) : Result<bool>.Fail(Error.Failure("Failed to delete the basket."));
        }

        public async Task<Result<BasketDto>> GetBasketAsync(string Id, CancellationToken ct = default)
        {
        var basket = await basketRepository.GetBasketAsync(Id, ct: ct);
            if (basket==null)
            {
                return Result<BasketDto>.Fail(Error.NotFound("Basketnot Found"));
            }
            return mapper.Map<BasketDto>(basket);
        }
    }
}
