using LibrarySTIVE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartieAPI_Projet_Solo.Data;

namespace PartieAPI_Projet_Solo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientSTIVEController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public ClientSTIVEController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllClients()
    {
        var clients = await _dbContext.ClientSTIVE.ToListAsync();
        return Ok(clients);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetClientById(int id)
    {
        var client = await _dbContext.ClientSTIVE.FindAsync(id);
        if (client == null)
            return NotFound(new { message = "Client not found" });

        return Ok(client);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateClient([FromBody] ClientSTIVE client)
    {
        if (client == null || string.IsNullOrWhiteSpace(client.Name))
            return BadRequest(new { message = "Invalid client data" });

        _dbContext.ClientSTIVE.Add(client);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetClientById), new { id = client.Id }, client);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientSTIVE client)
    {
        if (client == null || string.IsNullOrWhiteSpace(client.Name))
            return BadRequest(new { message = "Invalid client data" });

        var existingClient = await _dbContext.ClientSTIVE.FindAsync(id);
        if (existingClient == null)
            return NotFound(new { message = "Client not found" });

        existingClient.Name = client.Name;
        await _dbContext.SaveChangesAsync();

        return Ok(existingClient);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _dbContext.ClientSTIVE.FindAsync(id);
        if (client == null)
            return NotFound(new { message = "Client not found" });

        _dbContext.ClientSTIVE.Remove(client);
        await _dbContext.SaveChangesAsync();

        return Ok(new { message = "Client deleted" });
    }
}
