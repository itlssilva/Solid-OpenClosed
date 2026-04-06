using Freight.Domain.Interfaces;
using Freight.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Freight.Infrastructure.Calculators;

public sealed class JadLogCalculator(ILogger<CorreiosCalculator> logger)
    : IFreightCalculator
{
    public string CarrierName => "JadLog";

    public async Task<FreightQuote> CalculateAsync(FreightRequest request, CancellationToken ct = default)
    {
        logger.LogInformation(
            "[JadLog] Calculando frete {Origin} → {Dest}",
            request.OriginZipCode, request.DestinationZipCode);

        // Aqui entraria a chamada real à API da JadLog
        await Task.Delay(60, ct);

        var cubicWeight = (request.LengthCm * request.HeightCm * request.WidthCm) / 6000m;
        var billableWeight = Math.Max(request.WeightKg, cubicWeight);
        var price = Math.Round(12m + billableWeight * 5.2m, 2);

        return new FreightQuote(CarrierName, price, 3);
    }
}
