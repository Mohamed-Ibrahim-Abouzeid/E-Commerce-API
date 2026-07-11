using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Contracts
{
    public interface ICasheService
    {
        Task<string?> GetAsync(string cashekey, CancellationToken ct = default);
        Task SetAsync(string cashekey, object casheValue, TimeSpan TimeToLive, CancellationToken ct = default);
    }
}
