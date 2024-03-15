using Microsoft.EntityFrameworkCore;

public class ParametersRepository : IParametersRepository
{
    private readonly MiniMesDbContext _dbContext;

    public ParametersRepository(MiniMesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<IParameters>> GetAllParameters()
    {
        var parameter = await _dbContext.Parameters.ToListAsync();
        return parameter.Cast<IParameters>().ToList();
    }

    public async Task AddParameters(IParameters parameter)
    {
        var concreteParameter = new Parameters
        {
            Id = parameter.Id,
            Name = parameter.Name,
            Unit = parameter.Unit,
        };

        _dbContext.Parameters.Add(concreteParameter);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteParameters(int id)
    {
        var parameter = await _dbContext.Parameters.FindAsync(id);
        _dbContext.Parameters.Remove(parameter);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateParameters(int id, IParameters updatedParametersData)
    {
        var parameter = await _dbContext.Parameters.FindAsync(id);

        if (parameter == null)
        {
            throw new ArgumentException("Parameters not found");
        }

        parameter.Name = updatedParametersData.Name;
        parameter.Unit = updatedParametersData.Unit;

        await _dbContext.SaveChangesAsync();
    }
}

