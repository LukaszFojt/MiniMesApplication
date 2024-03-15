public interface IProcesses
{
    int Id { get; set; }
    string SerialNumber { get; set; }
    string Status { get; set; }
    DateTime DateTime { get; set; }
    int OrderId { get; set; }
}

public interface IProcessesRepository
{
    Task<List<IProcesses>> GetAllProcesses();
    Task AddProcesses(IProcesses processes);
    Task DeleteProcesses(int id);
    Task UpdateProcesses(int id, IProcesses updatedProcessesData);
}

