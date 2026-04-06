using Freight.Domain.Models;

namespace Freight.Infrastructure.Services;

public interface IViaCepService
{
    Task<Address?> GetAddressAsync(string zipCode, CancellationToken ct = default);
}
