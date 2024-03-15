using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProcessParametersController : ControllerBase
{
    private readonly IProcessParametersRepository _processProcessParametersRepository;

    public ProcessParametersController(IProcessParametersRepository processProcessParametersRepository)
    {
        _processProcessParametersRepository = processProcessParametersRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProcessParameters>>> GetAllProcessParameterss()
    {
        var processProcessParameters = await _processProcessParametersRepository.GetAllProcessParameters();
        return Ok(processProcessParameters);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddProcessParameters([FromBody] ProcessParameters processProcessParameters)
    {
        await _processProcessParametersRepository.AddProcessParameters(processProcessParameters);
        return Ok("Rekord " + processProcessParameters + " został dodany.");
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteProcessParameters([FromRoute] int id)
    {
        await _processProcessParametersRepository.DeleteProcessParameters(id);
        return Ok("ProcessParameters with ID " + id + " has been deleted.");
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateProcessParameters(int id, [FromBody] ProcessParameters updatedProcessParametersData)
    {
        await _processProcessParametersRepository.UpdateProcessParameters(id, updatedProcessParametersData);
        return Ok("ProcessParameters with ID " + id + " has been updated.");
    }
}
