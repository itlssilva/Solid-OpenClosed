using Freight.Domain.Interfaces;
using Freight.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Freight.Infrastructure.Calculators;

public sealed class CorreiosCalculator(ILogger<CorreiosCalculator> logger)
    : IFreightCalculator
{
    public string CarrierName => "Correios";

    public async Task<FreightQuote> CalculateAsync(FreightRequest request, CancellationToken ct = default)
    {
        logger.LogInformation(
            "[Correios] Calculando frete {Origin} → {Dest}",
            request.OriginZipCode, request.DestinationZipCode);

        // Aqui entraria a chamada real ao Webservice dos Correios (SIGEP / REST)
        // Simulação baseada em peso para fins de aprendizado
        await Task.Delay(80, ct);

        var price = Math.Round(15m + request.WeightKg * 4.5m, 2);
        var days = request.WeightKg > 5 ? 7 : 5;

        return new FreightQuote(CarrierName, price, days);
    }
}
