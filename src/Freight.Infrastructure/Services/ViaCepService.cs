using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Freight.Domain.Models;

namespace Freight.Infrastructure.Services;

internal sealed class ViaCepService(HttpClient http) : IViaCepService
{
    public async Task<Address?> GetAddressAsync(string zipCode, CancellationToken ct = default)
    {
        var clean = zipCode.Replace("-", "").Trim();
        var response = await http.GetFromJsonAsync<ViaCepResponse>(
            $"https://viacep.com.br/ws/{clean}/json/", ct);

        if (response is null || response.Erro == true)
            return null;

        return new Address(response.Cep!, response.Localidade!, response.Uf!);
    }

    private sealed record ViaCepResponse(
        string? Cep,
        string? Localidade,
        string? Uf,
        [property: JsonPropertyName("erro")] bool? Erro
    );
}
