using Freight.Domain.Models;

namespace Freight.Domain.Interfaces;

public interface IFreightCalculator
{
    /// <summary>Identificador legível da transportadora.</summary>
    string CarrierName { get; }

    Task<FreightQuote> CalculateAsync(FreightRequest request, CancellationToken ct = default);
}