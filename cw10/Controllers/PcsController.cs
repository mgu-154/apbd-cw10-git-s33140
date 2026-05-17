using cw10.DTOs;
using cw10.Exceptions;
using Microsoft.AspNetCore.Mvc;
using cw10.Services;

namespace cw10.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PcsController : ControllerBase
{
    private readonly IDbService _dbService;

    public PcsController(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllPcs()
    {
        var pcs = await _dbService.GetAllPcs();
        
        return Ok(pcs);
    }

    [HttpGet("{id:int}/components")]
    public async Task<IActionResult> GetComponents([FromRoute] int id)
    {
        var components = await _dbService.GetComponents(id);
        
        return Ok(components);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePc([FromBody] CreatePcDto pc)
    {
        var createdPc = await _dbService.CreatePc(pc);
        
        return Created(nameof(CreatePc), createdPc);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdatePc([FromRoute] int id, [FromBody] UpdatePcDto pc)
    {
        try
        {
            var updatedPc = await _dbService.UpdatePc(id, pc);

            return Ok(updatedPc);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePc([FromRoute] int id)
    {
        try
        {
            await _dbService.DeletePc(id);
            return NoContent();
        } catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}