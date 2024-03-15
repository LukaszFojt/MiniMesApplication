public interface IProcessParameters
{
    int Id { get; set; }
    double Value { get; set; }
    int ProcessId { get; set; }
    int ParameterId { get; set; }
}

public interface IProcessParametersRepository
{
    Task<List<IProcessParameters>> GetAllProcessParameters();
    Task AddProcessParameters(IProcessParameters processParameters);
    Task DeleteProcessParameters(int id);
    Task UpdateProcessParameters(int id, IProcessParameters updatedProcessParametersData);
}
