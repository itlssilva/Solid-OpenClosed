namespace Freight.Domain.Models;

public record FreightRequest(
    string OriginZipCode,
    string DestinationZipCode,
    decimal WeightKg,
    decimal LengthCm,
    decimal HeightCm,
    decimal WidthCm
);
