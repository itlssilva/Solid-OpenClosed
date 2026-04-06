namespace Freight.Domain.Models;

public record Address(
    string ZipCode,
    string City,
    string State
);
