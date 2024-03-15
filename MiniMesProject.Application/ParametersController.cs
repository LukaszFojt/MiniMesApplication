using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ParametersController : ControllerBase
{
    private readonly IParametersRepository _parametersRepository;

    public ParametersController(IParametersRepository parametersRepository)
    {
        _parametersRepository = parametersRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Parameters>>> GetAllParameterss()
    {
        var parameters = await _parametersRepository.GetAllParameters();
        return Ok(parameters);
    }

    [HttpPost]
    [Route("Add")]
    public async Task<IActionResult> AddParameters([FromBody] Parameters parameter)
    {
        await _parametersRepository.AddParameters(parameter);
        return Ok("Rekord " + parameter + " został dodany.");
    }

    [HttpDelete]
    [Route("Delete/{id}")]
    public async Task<IActionResult> DeleteParameters([FromRoute] int id)
    {
        await _parametersRepository.DeleteParameters(id);
        return Ok("Parameters with ID " + id + " has been deleted.");
    }

    [HttpPut]
    [Route("Update/{id}")]
    public async Task<IActionResult> UpdateParameters(int id, [FromBody] Parameters updatedParametersData)
    {
        await _parametersRepository.UpdateParameters(id, updatedParametersData);
        return Ok("Parameters with ID " + id + " has been updated.");
    }
}
