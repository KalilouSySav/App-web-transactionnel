using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using tpFinalA16.Models;
using tpFinalA16.Util;
using tpFinalA16.Util.Utils;

namespace tpFinalA16.Controllers
{
    public class ProduitController : Controller
    {
        private IProduitRepository _produitRepository;

        public ProduitController(IProduitRepository produitRepository)
        {
            _produitRepository = produitRepository;
        }
        public IActionResult RechercherNom(string nomProduit)
        {
            List<LigneDeCommande> buylist = new List<LigneDeCommande>();

            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData) && cartData != null)
            {
                // Deserialize the cart data from bytes to your list type
                buylist = JsonSerializer.Deserialize<List<LigneDeCommande>>(cartData);
            }
            ViewData["shoppingCart"] = buylist;
            ViewData["compteurPanier"] = buylist.Count;

            RechercheProduit rechercheProduit = new RechercheProduit();
            List<Produit> produitsTrouvees = new List<Produit>();
            List<string> imagePaths = new List<string>();
            List<Produit> produits = new List<Produit>();

            produits = _produitRepository.GetAllProduits();
            produitsTrouvees = rechercheProduit.SearchByName(nomProduit, produits);

            if (produitsTrouvees.Count == 0)
            {
                return View("~/Views/Home/pageAucunResultat.cshtml");
            }
            else
            {
                List<string> categories = _produitRepository.GetCategories();

                ViewBag.Categories = categories;
                imagePaths = _produitRepository.GetImagePaths();
                ViewData["produitsOrdonnees"] = produitsTrouvees;
                ViewData["imagePaths"] = imagePaths;
                return View("~/Views/Home/pageAfficherListeProduits.cshtml");
            }
        }

        public IActionResult TraitementProduitDetail(int id)
        {

            List<LigneDeCommande> buylist = new List<LigneDeCommande>();

            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData) && cartData != null)
            {
                // Deserialize the cart data from bytes to your list type
                buylist = JsonSerializer.Deserialize<List<LigneDeCommande>>(cartData);
            }
            ViewData["shoppingCart"] = buylist;
            ViewData["compteurPanier"] = buylist.Count;

            List<string> imagePaths = new List<string>();
            imagePaths = _produitRepository.GetImagePaths();
            Produit produit = new Produit();
            produit = _produitRepository.GetProduit(id);

            string imagePath = imagePaths[id-1];

            ViewData["imagePath"] = imagePath;
            ViewData["produit"] = produit;

            return View("~/Views/Home/pageProduitDetail.cshtml");
        }

        public IActionResult TraitementRechercheProduit(string filtreCategorie, int prixMin, int prixMax, string alphabet, string tri, bool favoris, string nomProduit, bool stockDisponible)
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
            List<string> imagePaths = new List<string>();
            List<string> categories = new List<string>();

            produits = _produitRepository.GetAllProduits();

            if (filtreCategorie != null)
            {
                produits = rechercheProduit.SearchByCategory(filtreCategorie, produits);
            }
            if (prixMax != 0 || prixMin != 0)
            {
                produits = rechercheProduit.FilterByPriceRange(produits, prixMin, prixMax);
            }
            if (alphabet != null)
            {
                produits = rechercheProduit.FilterByStartingLetter(produits, alphabet);
            }
            if (favoris == true)
            {
                //Procédure stockée
                produits = rechercheProduit.FilterByFavorites();
            }
            if (stockDisponible == true)
            {
                produits = rechercheProduit.FilterByAvailableStock(produits);
            }
            if (nomProduit != null)
            {
                produits = rechercheProduit.SearchByName(nomProduit, produits);
            }
            if (tri != null)
            {
                produits = rechercheProduit.SortProducts(produits, tri);
            }


            if (produits.Count == 0)
            {
                return View("~/Views/Home/pageAucunResultat.cshtml");
            }
            else
            {
                categories = _produitRepository.GetCategories();

                ViewBag.Categories = categories;
                imagePaths = _produitRepository.GetImagePaths();
                ViewData["produitsOrdonnees"] = produits;
                ViewData["imagePaths"] = imagePaths;
                return View("~/Views/Home/pageAfficherListeProduits.cshtml");
            }
        }
    }
}
