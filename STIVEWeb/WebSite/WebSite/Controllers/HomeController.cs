using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebSite.Services;
using LibrarySTIVE.Models;


namespace WebSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var articles = await ApiClient.GetAllArcticle();
            return View(articles);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching articles");
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    [Route("ProductDetails/{id}")]
    public async Task<IActionResult> ProductDetails(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Invalid ID provided: {Id}", id);
            return BadRequest("Invalid article ID.");
        }

        try
        {
            ModelArticleJson article = await ApiClient.GetArticleBy(id);
            if (article == null)
            {
                return NotFound("Article not found.");
            }
            return View(article);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching article");
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [Route("cart/{id}")]
    public async Task<IActionResult> Commande(int id)
    {
        if (id <= 0)
        {
            _logger.LogWarning("Invalid ID provided: {Id}", id);
            return BadRequest("Invalid commande ID.");
        }

        try
        {
            ModelCommandeJson commande = await ApiCommande.GetById(id);
            if (commande == null)
            {
                return NotFound("Commande not found.");
            }
            return View(commande); // Return the local variable 'commande'
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching commande");
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }






}