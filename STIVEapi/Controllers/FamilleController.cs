using Microsoft.AspNetCore.Mvc;
using PartieAPI_Projet_Solo.Data;
using LibrarySTIVE.Models;

namespace PartieAPI_Projet_Solo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamilleController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public FamilleController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllFamilles()
        {
            return Ok(_dbContext.Familles.ToList());
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetFamilleById(int id)
        {
            var famille = _dbContext.Familles.Find(id);
            return famille != null ? Ok(famille) : NotFound();
        }

        [HttpPost("Create")]
        public IActionResult CreateFamille(Famille famille)
        {
            _dbContext.Familles.Add(famille);
            _dbContext.SaveChanges();
            return Ok(famille);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateFamille(int id, Famille famille)
        {
            var existingFamille = _dbContext.Familles.Find(id);
            if (existingFamille == null) return NotFound();

            existingFamille.Name = famille.Name;
            _dbContext.SaveChanges();
            return Ok(existingFamille);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteFamille(int id)
        {
            var famille = _dbContext.Familles.Find(id);
            if (famille == null) return NotFound();

            _dbContext.Familles.Remove(famille);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
