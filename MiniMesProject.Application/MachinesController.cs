
using Microsoft.AspNetCore.Mvc;


/**
 * Kontroler obsługujący operacje na maszynach.
 */
[ApiController]
[Route("api/[controller]")]
public class MachinesController : ControllerBase
{
    private readonly IMachineRepository _machineRepository;

    /**
     * Konstruktor klasy MachinesController.
     * @param machineRepository Repozytorium maszyn.
     */
    public MachinesController(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }

    /**
     * Metoda asynchroniczna pobierająca listę wszystkich maszyn.
     * @return Lista maszyn.
     */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Machine>>> GetAllMachines()
    {
        var machines = await _machineRepository.GetAllMachines();
        return Ok(machines);
    }

    /**
     * Metoda asynchroniczna dodająca nową maszynę.
     * @param machine Obiekt reprezentujący nową maszynę.
     * @return Informacja o dodanej maszynie.
     */
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddMachine([FromBody] Machine machine)
    {
        await _machineRepository.AddMachine(machine);
        return Ok("Rekord " + machine + " został dodany.");
    }

    /**
     * Metoda asynchroniczna usuwająca maszynę o podanym identyfikatorze.
     * @param id Identyfikator maszyny do usunięcia.
     * @return Informacja o usunięciu maszyny.
     */
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteMachine([FromRoute] int id)
    {
        await _machineRepository.DeleteMachine(id);
        return Ok("Machine with ID " + id + " has been deleted.");
    }

    /**
     * Metoda asynchroniczna aktualizująca dane maszyny.
     * @param id Identyfikator maszyny do zaktualizowania.
     * @param updatedMachineData Nowe dane maszyny.
     * @return Informacja o zaktualizowanej maszynie.
     */
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateMachine(int id, [FromBody] Machine updatedMachineData)
    {
        await _machineRepository.UpdateMachine(id, updatedMachineData);
        return Ok("Machine with ID " + id + " has been updated.");
    }
}
