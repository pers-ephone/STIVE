using Microsoft.AspNetCore.Mvc;
using PartieAPI_Projet_Solo.Data;
using LibrarySTIVE.Models;

namespace PartieAPI_Projet_Solo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FournisseurController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public FournisseurController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllFournisseurs()
        {
            return Ok(_dbContext.Fournisseurs.ToList());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetFournisseurById(int id)
        {
            var fournisseur = _dbContext.Fournisseurs.Find(id);
            return fournisseur != null ? Ok(fournisseur) : NotFound();
        }

        [HttpPost("Create")]
        public IActionResult CreateFournisseur(Fournisseur fournisseur)
        {
            _dbContext.Fournisseurs.Add(fournisseur);
            _dbContext.SaveChanges();
            return Ok(fournisseur);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateFournisseur(int id, Fournisseur fournisseur)
        {
            var existingFournisseur = _dbContext.Fournisseurs.Find(id);
            if (existingFournisseur == null) return NotFound();

            existingFournisseur.Name = fournisseur.Name;
            existingFournisseur.Adresse = fournisseur.Adresse;

            _dbContext.SaveChanges();
            return Ok(existingFournisseur);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteFournisseur(int id)
        {
            var fournisseur = _dbContext.Fournisseurs.Find(id);
            if (fournisseur == null) return NotFound();

            _dbContext.Fournisseurs.Remove(fournisseur);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
