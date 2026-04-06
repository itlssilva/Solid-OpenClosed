using Freight.Domain.Models;

namespace Freight.Application.Services;

public interface IFreightService
{
    Task<IReadOnlyList<FreightQuote>> GetAllQuotesAsync(FreightRequest request, CancellationToken ct = default);
    Task<FreightQuote> GetCheapestAsync(FreightRequest request, CancellationToken ct = default);
}
