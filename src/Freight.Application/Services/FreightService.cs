using Freight.Domain.Interfaces;
using Freight.Domain.Models;
using Freight.Infrastructure.Services;

namespace Freight.Application.Services;

internal class FreightService(
    IEnumerable<IFreightCalculator> calculators,
    IViaCepService viaCep) : IFreightService
{
    public async Task<IReadOnlyList<FreightQuote>> GetAllQuotesAsync(FreightRequest request, CancellationToken ct = default)
    {
        // Valida os CEPs via ViaCEP antes de calcular
        var origin = await viaCep.GetAddressAsync(request.OriginZipCode, ct)
            ?? throw new ArgumentException($"CEP de origem inválido: {request.OriginZipCode}");

        var dest = await viaCep.GetAddressAsync(request.DestinationZipCode, ct)
            ?? throw new ArgumentException($"CEP de destino inválido: {request.DestinationZipCode}");

        // Todas as transportadoras calculam em paralelo
        // Adicionar uma nova = registrá-la no DI. Nada aqui muda.
        var tasks = calculators.Select(c => c.CalculateAsync(request, ct));
        var quotes = await Task.WhenAll(tasks);

        return quotes.OrderBy(q => q.PriceBrl).ToList();
    }

    public async Task<FreightQuote> GetCheapestAsync(FreightRequest request, CancellationToken ct = default)
    {
        var all = await GetAllQuotesAsync(request, ct);
        return all[0]; // já ordenado por preço
    }
}
