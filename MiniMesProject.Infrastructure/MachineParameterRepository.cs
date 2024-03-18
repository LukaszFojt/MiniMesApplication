using Microsoft.EntityFrameworkCore;

/**
 * Repozytorium odpowiadające za operacje na danych maszyn.
 */
public class MachineParameterRepository : IMachineParameterRepository
{
    private readonly MiniMesDbContext _dbContext;

    /**
     * Konstruktor klasy MachineRepository.
     * @param dbContext Kontekst bazy danych MiniMesDbContext.
     */
    public MachineParameterRepository(MiniMesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /**
     * Metoda asynchroniczna pobierająca listę wszystkich maszyn.
     * @return Lista maszyn.
     
    public async Task<List<IMachineParameter>> GetAllMachinesParameter()
    {
        var machineParameter = await _dbContext.MachineParameter.ToListAsync();
        return machineParameter.Cast<IMachineParameter>().ToList();
    }
    */

    /**
     * Metoda asynchroniczna dodająca nową maszynę.
     * @param machine Obiekt reprezentujący nową maszynę.
     */
    public async Task AddMachineParameter(IMachineParameter machineParameter)
    {
        var concreteMachineParameter = new MachineParameter
        {
            Id = machineParameter.Id,
            DateTime = machineParameter.DateTime,
            MachineId = machineParameter.MachineId,
            ParameterId = machineParameter.ParameterId,
        };

        _dbContext.MachineParameter.Add(concreteMachineParameter);
        await _dbContext.SaveChangesAsync();
    }

    /**
     * Metoda asynchroniczna usuwająca maszynę o podanym identyfikatorze.
     * @param id Identyfikator maszyny do usunięcia.
     */
    public async Task DeleteMachineParameter(int id)
    {
        var machineParameter = await _dbContext.MachineParameter.FindAsync(id);
        _dbContext.MachineParameter.Remove(machineParameter);
        await _dbContext.SaveChangesAsync();
    }

    /**
     * Metoda asynchroniczna aktualizująca dane maszyny.
     * @param id Identyfikator maszyny do zaktualizowania.
     * @param updatedMachineData Nowe dane maszyny.
     */
    public async Task UpdateMachineParameter(int id, IMachineParameter updatedMachineParameterData)
    {
        var machineParameter = await _dbContext.MachineParameter.FindAsync(id);

        if (machineParameter == null)
        {
            throw new ArgumentException("Machine not found");
        }

        // Aktualizujemy właściwości obiektu Machine na podstawie danych z IMachine
        machineParameter.DateTime = updatedMachineParameterData.DateTime;
        machineParameter.MachineId = updatedMachineParameterData.MachineId;
        machineParameter.ParameterId = updatedMachineParameterData.ParameterId;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<IMachineParameter>> GetParametersByMachineId(int machineId)
    {
        var machineParameters = await _dbContext.MachineParameter
            .Where(mp => mp.MachineId == machineId)
            .ToListAsync();

        if (machineParameters.Count == 0)
        {
            throw new ArgumentException("Machine parameters not found for this machine ID");
        }

        return machineParameters.Cast<IMachineParameter>().ToList();
    }
}
