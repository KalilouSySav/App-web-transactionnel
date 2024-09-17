using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using tpFinalA16.Models;
using tpFinalA16.Util.Utils;

namespace tpFinalA16.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProduitRepository _produitRepository;

        public HomeController(ILogger<HomeController> logger, IProduitRepository produitRepository)
        {
            _logger = logger;
            _produitRepository = produitRepository;
        }
        
        public IActionResult pageHome()
        {
            List<LigneDeCommande> buylist = new List<LigneDeCommande>();

            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData) && cartData != null)
            {
                // Deserialize the cart data from bytes to your list type
                buylist = JsonSerializer.Deserialize<List<LigneDeCommande>>(cartData);
            }
            ViewData["compteurPanier"] = buylist.Count;
            ViewData["shoppingCart"] = buylist;

            List<Produit> produitsDecouverts = new List<Produit>();
            produitsDecouverts = _produitRepository.GetProduitsDecouverts();

            List<string> imagePaths = new List<string>();
            imagePaths = _produitRepository.GetImagePaths();

            var viewModel = new PageHomeViewModel
            {
                ProduitsDecouverts = produitsDecouverts,
                ImagePaths = imagePaths
            };

            List<string> categories = _produitRepository.GetCategories();

            ViewBag.Categories = categories;
            ViewBag.ProduitsDecouverts = produitsDecouverts;
            ViewBag.ImagePaths = imagePaths;

            return View(viewModel);
        }

        public IActionResult pageAfficherListeProduits()
        {

            List<LigneDeCommande> buylist = new List<LigneDeCommande>();

            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData) && cartData != null)
            {
                // Deserialize the cart data from bytes to your list type
                buylist = JsonSerializer.Deserialize<List<LigneDeCommande>>(cartData);
            }
            ViewData["compteurPanier"] = buylist.Count;
            ViewData["shoppingCart"] = buylist;

            RechercheProduit rechercheProduit = new RechercheProduit();
            List<Produit> produits = new List<Produit>();
            List<Produit> produitsOrdonnees = new List<Produit>();
            List<string> imagePaths = new List<string>();
            imagePaths = _produitRepository.GetImagePaths();
            produits = _produitRepository.GetAllProduits();
            produitsOrdonnees = rechercheProduit.SortProducts(produits, "nameAsc");
            ViewData["produitsOrdonnees"] = produitsOrdonnees;
            ViewData["imagePaths"] = imagePaths;

            List<string> categories = _produitRepository.GetCategories();

            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult pageContact()
        {

            List<LigneDeCommande> buylist = new List<LigneDeCommande>();

            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData) && cartData != null)
            {
                // Deserialize the cart data from bytes to your list type
                buylist = JsonSerializer.Deserialize<List<LigneDeCommande>>(cartData);
            }
            ViewData["compteurPanier"] = buylist.Count;
            ViewData["shoppingCart"] = buylist;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}