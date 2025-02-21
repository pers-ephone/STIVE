using LibrarySTIVE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartieAPI_Projet_Solo.Data;

namespace PartieAPI_Projet_Solo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommandeController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public CommandeController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAllCommandes()
    {
        var commandes = await _dbContext.Commande
            .Include(c => c.Articles)
            .ThenInclude(ac => ac.Article)
            .ToListAsync();

        var commandesDTO = commandes.Select(c => new CommandeDTO
        {
            Id = c.Id,
            IdClient = c.IdClient,
            TotalPrice = c.TotalPrice,
            Status = c.Status,
            CreatedAt = c.CreatedAt,
            Client = c.IdClient,
            Articles = c.Articles?.Select(a => a.IdArticle).ToList() ?? new List<int>()
        });

        return Ok(commandesDTO);
    }

    [HttpGet("GetById/{id}")]
    public async Task<IActionResult> GetCommandeById(int id)
    {
        var commande = await _dbContext.Commande
            .Include(c => c.Articles)
            .ThenInclude(a => a.Article)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (commande == null)
            return NotFound(new { message = "Commande not found" });

        var commandeDTO = new CommandeDTO
        {
            Id = commande.Id,
            IdClient = commande.IdClient,
            TotalPrice = commande.TotalPrice,
            Status = commande.Status,
            CreatedAt = commande.CreatedAt,
            Client = commande.IdClient,
            Articles = commande.Articles?.Select(a => a.IdArticle).ToList() ?? new List<int>()
        };

        return Ok(commandeDTO);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateCommande([FromBody] CommandeDTO commandeDTO)
    {
        if (commandeDTO == null)
            return BadRequest(new { message = "Commande cannot be null" });

        if (commandeDTO.IdClient <= 0)
            return BadRequest(new { message = "Invalid client ID" });

        var existingClient = await _dbContext.ClientSTIVE.FindAsync(commandeDTO.IdClient);
        if (existingClient == null)
            return BadRequest(new { message = "Client does not exist" });

        var newCommande = new Commande
        {
            IdClient = commandeDTO.IdClient,
            TotalPrice = commandeDTO.TotalPrice,
            Status = commandeDTO.Status,
            CreatedAt = commandeDTO.CreatedAt
        };

        _dbContext.Commande.Add(newCommande);
        await _dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCommandeById), new { id = newCommande.Id }, newCommande);
    }

    [HttpPost("AddArticle")]
    public async Task<IActionResult> AddArticleToCommande([FromBody] ArticlesInCommandeDTO articleInCommandeDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var existingCommande = await _dbContext.Commande.FindAsync(articleInCommandeDTO.IdCommande);
        if (existingCommande == null)
            return BadRequest("Invalid Commande ID");

        foreach (var articleId in articleInCommandeDTO.Article)
        {
            var existingArticle = await _dbContext.Articles.FindAsync(articleId);
            if (existingArticle == null)
                return BadRequest($"Invalid Article ID: {articleId}");

            var newArticleInCommande = new ArticlesInCommande
            {
                IdCommande = articleInCommandeDTO.IdCommande,
                IdArticle = articleId,
                Quantity = articleInCommandeDTO.Quantity,
                Price = articleInCommandeDTO.Price
            };

            _dbContext.ArticlesInCommande.Add(newArticleInCommande);
        }

        await _dbContext.SaveChangesAsync();
        return Ok(articleInCommandeDTO);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteCommande(int id)
    {
        var commande = await _dbContext.Commande.FindAsync(id);
        if (commande == null)
            return NotFound(new { message = "Commande not found" });

        var articlesInCommande = _dbContext.ArticlesInCommande.Where(a => a.IdCommande == id);
        _dbContext.ArticlesInCommande.RemoveRange(articlesInCommande);
        _dbContext.Commande.Remove(commande);

        await _dbContext.SaveChangesAsync();
        return Ok(new { message = "Commande deleted" });
    }
}
