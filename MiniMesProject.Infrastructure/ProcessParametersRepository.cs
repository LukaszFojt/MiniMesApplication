using Microsoft.EntityFrameworkCore;

public class ProcessParametersRepository : IProcessParametersRepository
{
    private readonly MiniMesDbContext _dbContext;

    public ProcessParametersRepository(MiniMesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IProcessParameters>> GetAllProcessParameters()
    {
        var processParameters = await _dbContext.ProcessParameters.ToListAsync();
        return processParameters.Cast<IProcessParameters>().ToList();
    }

    public async Task AddProcessParameters(IProcessParameters processParameters)
    {
        var concreteProcessParameter = new ProcessParameters
        {
            Id = processParameters.Id,
            Value = processParameters.Value,
            ProcessId = processParameters.ProcessId,
            ParameterId = processParameters.ParameterId,
        };

        _dbContext.ProcessParameters.Add(concreteProcessParameter);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteProcessParameters(int id)
    {
        var processParameters = await _dbContext.ProcessParameters.FindAsync(id);
        _dbContext.ProcessParameters.Remove(processParameters);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProcessParameters(int id, IProcessParameters updatedProcessParametersData)
    {
        var processParameters = await _dbContext.ProcessParameters.FindAsync(id);

        if (processParameters == null)
        {
            throw new ArgumentException("ProcessParameters not found");
        }

        processParameters.Value = updatedProcessParametersData.Value;
        processParameters.ParameterId = updatedProcessParametersData.ParameterId;
        processParameters.ProcessId = updatedProcessParametersData.ProcessId;

        await _dbContext.SaveChangesAsync();
    }
}

