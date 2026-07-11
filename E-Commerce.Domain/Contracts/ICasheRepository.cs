using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Domain.Contracts
{
    public interface ICasheRepository
    {
        Task<string> GetAsync(string casheKey, CancellationToken ct = default);
        Task SetAsync(string casheKey, string value, TimeSpan TimeToLive, CancellationToken ct = default);
    }
}
