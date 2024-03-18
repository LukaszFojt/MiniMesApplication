
using Microsoft.AspNetCore.Mvc;


/**
 * Kontroler obsługujący operacje na maszynach.
 */
[ApiController]
[Route("api/[controller]")]
public class MachineParameterController : ControllerBase
{
    private readonly IMachineParameterRepository _machineParameterRepository;

    /**
     * Konstruktor klasy MachinesController.
     * @param machineRepository Repozytorium maszyn.
     */
    public MachineParameterController(IMachineParameterRepository machineParameterRepository)
    {
        _machineParameterRepository = machineParameterRepository;
    }

    /**
     * Metoda asynchroniczna pobierająca listę wszystkich maszyn.
     * @return Lista maszyn.
     
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MachineParameter>>> GetAllMachinesParameter()
    {
        var machinesParameter = await _machineParameterRepository.GetAllMachinesParameter();
        return Ok(machinesParameter);
    }
    */
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MachineParameter>>> GetParametersByMachineId(int machineId)
    {
        var machinesParameter = await _machineParameterRepository.GetParametersByMachineId(machineId);
        return Ok(machinesParameter);
    }

    /**
     * Metoda asynchroniczna dodająca nową maszynę.
     * @param machine Obiekt reprezentujący nową maszynę.
     * @return Informacja o dodanej maszynie.
     */
    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddMachineParameter([FromBody] MachineParameter machineParameter)
    {
        await _machineParameterRepository.AddMachineParameter(machineParameter);
        return Ok("Rekord " + machineParameter + " został dodany.");
    }

    /**
     * Metoda asynchroniczna usuwająca maszynę o podanym identyfikatorze.
     * @param id Identyfikator maszyny do usunięcia.
     * @return Informacja o usunięciu maszyny.
     */
    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteMachineParameter([FromRoute] int id)
    {
        await _machineParameterRepository.DeleteMachineParameter(id);
        return Ok("Machine Parameter with ID " + id + " has been deleted.");
    }

    /**
     * Metoda asynchroniczna aktualizująca dane maszyny.
     * @param id Identyfikator maszyny do zaktualizowania.
     * @param updatedMachineData Nowe dane maszyny.
     * @return Informacja o zaktualizowanej maszynie.
     */
    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateMachineParameter(int id, [FromBody] MachineParameter updatedMachineParameterData)
    {
        await _machineParameterRepository.UpdateMachineParameter(id, updatedMachineParameterData);
        return Ok("Machine Parameter with ID " + id + " has been updated.");
    }
}
