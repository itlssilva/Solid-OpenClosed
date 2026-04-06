namespace Freight.Domain.Models;

public record FreightQuote(
    string Carrier,       // "Correios", "JadLog", etc.
    decimal PriceBrl,
    int DeliveryDays
);
