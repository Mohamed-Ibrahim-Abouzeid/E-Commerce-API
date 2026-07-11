using E_Commerce.Application.Contracts;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace E_Commerce.Application.Services
{
    public class CasheService:ICasheService
    {
        private readonly ICasheRepository _casheRepository;
        public CasheService(ICasheRepository casheRepository)
        {
            _casheRepository = casheRepository;
        }
        public Task<string?> GetAsync(string cashekey, CancellationToken ct = default)
        =>_casheRepository.GetAsync(cashekey, ct);

        public Task SetAsync(string cashekey, object casheValue, TimeSpan TimeToLive, CancellationToken ct = default)
        {
            var json = JsonSerializer.Serialize(casheValue, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            return _casheRepository.SetAsync(cashekey, json, TimeToLive, ct);

        }
    }
}
