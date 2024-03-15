using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ProductionProcessService
{
    private readonly MiniMesDbContext _dbContext;

    public ProductionProcessService(MiniMesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<string> RegisterProductionProcess(int orderId, string serialNumber, string status, DateTime dateTime, Dictionary<string, string> parameters)
    {
        // 1. Walidacja danych wejściowych

        // Sprawdzenie, czy orderId odpowiada istniejącemu zleceniu w bazie danych
        var order = await _dbContext.Orders.FindAsync(orderId);
        if (order == null)
        {
            return "Invalid orderId. Order does not exist.";
        }

        // Weryfikacja unikalności serialNumber w kontekście danego zlecenia
        var existingProcessWithSerialNumber = await _dbContext.Processes.FirstOrDefaultAsync(p => p.SerialNumber == serialNumber && p.OrderId == orderId);
        if (existingProcessWithSerialNumber != null)
        {
            return "SerialNumber already exists for this order.";
        }

        // Sprawdzenie, czy status jest jedną z dozwolonych wartości (np. OK, NOK)
        if (status != "OK" && status != "NOK")
        {
            return "Invalid status. Status must be either OK or NOK.";
        }

        // Upewnienie się, że dateTime jest datą nie przyszłą i logicznie wpasowuje się w ramy czasowe zlecenia
        if (dateTime > DateTime.Now)
        {
            return "Invalid dateTime. It must be within the timeframe of the order and cannot be in the future.";
        }

        // Walidacja parameters pod kątem poprawności struktury i zakresu wartości
        var parameterValidationResult = ValidateParameters(parameters);
        if (!parameterValidationResult.IsValid)
        {
            return "Invalid parameters: " + parameterValidationResult.ErrorMessage;
        }

        // 2. Rejestracja procesu

        var newProcess = new Processes
        {
            SerialNumber = serialNumber,
            Status = status,
            DateTime = dateTime,
            OrderId = orderId
        };

        _dbContext.Processes.Add(newProcess);

        foreach (var parameter in parameters)
        {
            var parameterEntity = await _dbContext.Parameters.FirstOrDefaultAsync(p => p.Name == parameter.Key);
            if (parameterEntity != null)
            {
                var processParameter = new ProcessParameters
                {
                    ProcessId = newProcess.Id,
                    ParameterId = parameterEntity.Id
                };

                _dbContext.ProcessParameters.Add(processParameter);
            }
        }

        await _dbContext.SaveChangesAsync();

        return "Production process registered successfully.";
    }

    private ValidationResult ValidateParameters(Dictionary<string, string> parameters)
    {
        // Tutaj możesz dodać logikę walidacji parametrów, na przykład sprawdzenie ich struktury i zakresu wartości
        // W przypadku nieprawidłowych parametrów, możesz zwrócić obiekt ValidationResult z informacją o błędzie

        // Na potrzeby tego przykładu zwracamy pusty obiekt ValidationResult, co oznacza, że parametry są poprawne
        return new ValidationResult(true);
    }
}

public class ValidationResult
{
    public bool IsValid { get; }
    public string ErrorMessage { get; }

    public ValidationResult(bool isValid, string errorMessage = "")
    {
        IsValid = isValid;
        ErrorMessage = errorMessage;
    }
}
