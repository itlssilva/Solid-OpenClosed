using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Freight.Domain.Interfaces;
using Freight.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Freight.Infrastructure.Calculators;

public sealed class LoggiCalculator(ILogger<CorreiosCalculator> logger) : IFreightCalculator
{
    public string CarrierName => "Loggi";

    public async Task<FreightQuote> CalculateAsync(FreightRequest request, CancellationToken ct = default)
    {
        logger.LogInformation(
            "[Loggi] Calculando frete {Origin} → {Dest}",
            request.OriginZipCode, request.DestinationZipCode);

        await Task.Delay(40, ct);

        var price = Math.Round(10m + request.WeightKg * 6m, 2);
        return new FreightQuote(CarrierName, price, 2);
    }
}
