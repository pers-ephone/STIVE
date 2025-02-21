using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartieAPI_Projet_Solo.Data;
using LibrarySTIVE.Models;

namespace PartieAPI_Projet_Solo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ArticleController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllArticles()
        {
            var articles = _dbContext.Articles
                .Select(a => new
                {
                    a.Id,
                    a.Nom,
                    a.Contenance,
                    a.FamilleId,
                    a.FournisseurId,
                    a.Ref,
                    a.Prix,
                    a.Age,
                    a.ArtQuantity
                })
                .ToList();

            return Ok(articles);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetArticleById(int id)
        {
            var article = _dbContext.Articles
                .AsNoTracking()
                .Include(a => a.Famille)
                .Include(a => a.Fournisseur)
                .FirstOrDefault(a => a.Id == id);

            return article != null ? Ok(article) : NotFound(new { message = "Article not found" });
        }

        [HttpPost("Create")]
        public IActionResult CreateArticle([FromBody] Article article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var famille = _dbContext.Familles.Find(article.FamilleId);
            var fournisseur = _dbContext.Fournisseurs.Find(article.FournisseurId);

            if (famille == null || fournisseur == null)
                return BadRequest("Invalid FamilleId or FournisseurId");

            article.Famille = famille;
            article.Fournisseur = fournisseur;

            _dbContext.Articles.Add(article);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(GetArticleById), new { id = article.Id }, article);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateArticle(int id, [FromBody] Article article)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingArticle = _dbContext.Articles.Find(id);
            if (existingArticle == null)
                return NotFound(new { message = "Article not found" });

            existingArticle.Nom = article.Nom;
            existingArticle.Contenance = article.Contenance;
            existingArticle.FamilleId = article.FamilleId;
            existingArticle.FournisseurId = article.FournisseurId;
            existingArticle.Ref = article.Ref;
            existingArticle.Prix = article.Prix;
            existingArticle.Age = article.Age;
            existingArticle.ArtQuantity = article.ArtQuantity;

            _dbContext.SaveChanges();
            return Ok(existingArticle);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteArticle(int id)
        {
            var commandesToRemove = _dbContext.Commande.Where(c => c.Id == id);
            _dbContext.Commande.RemoveRange(commandesToRemove);
            var articleToRemove = _dbContext.Articles.Find(id);
            if (articleToRemove == null)
            {
                return NotFound(new { message = "ID NOT FIND" });
            }
            _dbContext.Articles.Remove(articleToRemove);
            _dbContext.SaveChanges();
            return Ok(new { message = "DELETED" });
        }


    }
}
