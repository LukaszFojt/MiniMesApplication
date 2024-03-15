using Microsoft.EntityFrameworkCore;

public class ProcessesRepository : IProcessesRepository
{
    private readonly MiniMesDbContext _dbContext;

    public ProcessesRepository(MiniMesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IProcesses>> GetAllProcesses()
    {
        var process = await _dbContext.Processes.ToListAsync();
        return process.Cast<IProcesses>().ToList();
    }

    public async Task AddProcesses(IProcesses processes)
    {
        var concreteProcess = new Processes
        {
            Id = processes.Id,
            SerialNumber = processes.SerialNumber,
            Status = processes.Status,
            DateTime = processes.DateTime,
            OrderId = processes.OrderId,
        };
        _dbContext.Processes.Add(concreteProcess);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProcesses(int id)
    {
        var process = await _dbContext.Processes.FindAsync(id);
        _dbContext.Processes.Remove(process);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProcesses(int id, IProcesses updatedProcessesData)
    {
        var process = await _dbContext.Processes.FindAsync(id);

        if (process == null)
        {
            throw new ArgumentException("Processes not found");
        }

        process.SerialNumber = updatedProcessesData.SerialNumber;
        process.Status = updatedProcessesData.Status;
        process.DateTime = updatedProcessesData.DateTime;
        process.OrderId = updatedProcessesData.OrderId;

        await _dbContext.SaveChangesAsync();
    }
}
