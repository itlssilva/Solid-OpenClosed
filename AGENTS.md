# AGENTS.md - Freight Quote System

## Architecture Overview
This is a Clean Architecture .NET application demonstrating the Open-Closed Principle through extensible freight calculators.

**Layers:**
- `Freight.Domain`: Core models (`FreightRequest`, `FreightQuote`, `Address`) and interfaces (`IFreightCalculator`)
- `Freight.Application`: Business logic (`FreightService` aggregates quotes from all calculators)
- `Freight.Infrastructure`: External concerns (calculators for Correios/JadLog/Loggi, ViaCep address validation)
- `Freight.Api`: Minimal API entry point

**Data Flow:** API receives `FreightRequest` → `FreightService` validates ZIP codes via ViaCep → parallel calculation across all registered `IFreightCalculator` implementations → returns sorted `FreightQuote` list.

## Key Patterns
- **Extensibility:** New carriers require only implementing `IFreightCalculator` and registering in `Program.cs` DI. No changes to existing code.
- **Dependency Injection:** All services use constructor injection. Calculators injected as `IEnumerable<IFreightCalculator>` for automatic discovery.
- **Validation:** ZIP codes validated against ViaCep API before calculation.
- **Parallel Processing:** All calculators run concurrently via `Task.WhenAll`.
- **Logging:** Structured logging in calculators with carrier-specific prefixes (e.g., `[Correios]`).

## Conventions
- Use records for immutable models (e.g., `FreightRequest(string OriginZipCode, ...)`).
- Interfaces in `Domain/Interfaces`, implementations in `Infrastructure`.
- Extension methods for DI registration (e.g., `services.AddInfrastructure()` in each layer).
- Primary constructors for services (e.g., `CorreiosCalculator(ILogger<CorreiosCalculator> logger)`).
- Simulated external APIs with `Task.Delay` for realistic async behavior.

## Workflows
- **Build:** `dotnet build` from solution root.
- **Run API:** `dotnet run --project src/Freight.Api` (listens on port 5036 by default).
- **Test Endpoints:** Use `Freight.Api.http` file in Rider/Visual Studio for requests (currently only `/test` endpoint exists).
- **Add Calculator:** Create class in `Infrastructure/Calculators` implementing `IFreightCalculator`, add `services.AddScoped<IFreightCalculator, NewCalculator>()` in `Program.cs`.

## Examples
- **Calculator Implementation:** See `CorreiosCalculator.cs` - price based on weight, fixed delivery days.
- **DI Registration:** Lines 19-21 in `Program.cs` register all calculators.
- **Service Aggregation:** `FreightService.GetAllQuotesAsync` validates ZIPs then calls all calculators in parallel.

## Dependencies
- ViaCep API for address validation (`https://viacep.com.br/ws/{zip}/json/`).
- Simulated carrier APIs (replace `Task.Delay` with real HTTP calls).</content>
<parameter name="filePath">D:\Repos\Git\Solid-OpenClosed\AGENTS.md
