using Microsoft.EntityFrameworkCore;

/**
 * Repozytorium odpowiadające za operacje na danych maszyn.
 */
public class MachineRepository : IMachineRepository
{
    private readonly MiniMesDbContext _dbContext;

    /**
     * Konstruktor klasy MachineRepository.
     * @param dbContext Kontekst bazy danych MiniMesDbContext.
     */
    public MachineRepository(MiniMesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /**
     * Metoda asynchroniczna pobierająca listę wszystkich maszyn.
     * @return Lista maszyn.
     */
    public async Task<List<IMachine>> GetAllMachines()
    {
        var machine = await _dbContext.Machines.ToListAsync();
        return machine.Cast<IMachine>().ToList();
    }

    /**
     * Metoda asynchroniczna dodająca nową maszynę.
     * @param machine Obiekt reprezentujący nową maszynę.
     */
    public async Task AddMachine(IMachine machine)
    {
        var concreteMachine = new Machine
        {
            Id = machine.Id,
            Name = machine.Name,
            Description = machine.Description
        };

        _dbContext.Machines.Add(concreteMachine);
        await _dbContext.SaveChangesAsync();
    }

    /**
     * Metoda asynchroniczna usuwająca maszynę o podanym identyfikatorze.
     * @param id Identyfikator maszyny do usunięcia.
     */
    public async Task DeleteMachine(int id)
    {
        var machine = await _dbContext.Machines.FindAsync(id);
        _dbContext.Machines.Remove(machine);
        await _dbContext.SaveChangesAsync();
    }

    /**
     * Metoda asynchroniczna aktualizująca dane maszyny.
     * @param id Identyfikator maszyny do zaktualizowania.
     * @param updatedMachineData Nowe dane maszyny.
     */
    public async Task UpdateMachine(int id, IMachine updatedMachineData)
    {
        var machine = await _dbContext.Machines.FindAsync(id);

        if (machine == null)
        {
            throw new ArgumentException("Machine not found");
        }

        // Aktualizujemy właściwości obiektu Machine na podstawie danych z IMachine
        machine.Name = updatedMachineData.Name;
        machine.Description = updatedMachineData.Description;

        await _dbContext.SaveChangesAsync();
    }
}
