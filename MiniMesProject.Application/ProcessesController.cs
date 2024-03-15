using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProcessessController : ControllerBase
{
    private readonly IProcessesRepository _processesRepository;

    public ProcessessController(IProcessesRepository processesRepository)
    {
        _processesRepository = processesRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Processes>>> GetAllProcesses()
    {
        var processess = await _processesRepository.GetAllProcesses();
        return Ok(processess);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddProcesses([FromBody] Processes process)
    {
        await _processesRepository.AddProcesses(process);
        return Ok("Rekord " + process + " został dodany.");
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteProcesses([FromRoute] int id)
    {
        await _processesRepository.DeleteProcesses(id);
        return Ok("Processes with ID " + id + " has been deleted.");
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateProcesses(int id, [FromBody] Processes updatedProcessesData)
    {
        await _processesRepository.UpdateProcesses(id, updatedProcessesData);
        return Ok("Processes with ID " + id + " has been updated.");
    }
}
