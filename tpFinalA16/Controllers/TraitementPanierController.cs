using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using tpFinalA16.Models;
using tpFinalA16.Util.Utils;
using System.Text.Json;

namespace tpFinalA16.Controllers
{
    public class TraitementPanierController : Controller
    {
        private IProduitRepository _produitRepository;

        public TraitementPanierController(IProduitRepository produitRepository)
        {
            _produitRepository = produitRepository;
        }

        public ActionResult ProcessRequest(string action, string delindex, string qty, string id)
        {

            ShoppingCartViewModel viewModel = new ShoppingCartViewModel();

            List<LigneDeCommande> buylist = new List<LigneDeCommande>();
            long quantity;

            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData) && cartData != null)
            {
                // Deserialize the cart data from bytes to your list type
                buylist = JsonSerializer.Deserialize<List<LigneDeCommande>>(cartData);
            }
            if (!string.IsNullOrEmpty(action))
            {
                if (action.Equals("DELETE"))
                {
                    if (!string.IsNullOrEmpty(delindex))
                    {
                        int d = Convert.ToInt32(delindex);
                        if (d >= 0 && d < buylist.Count)
                        {
                            buylist.RemoveAt(d);
                        }
                    }
                }
                else if (action.Equals("ADD"))
                {
                    if (!string.IsNullOrEmpty(qty) && !string.IsNullOrEmpty(id))
                    {
                        quantity = Convert.ToInt64(qty);
                        bool match = false;
                        LigneDeCommande ligneDeCommande = GetLigneDeCommande(id, quantity);

                        if (quantity > ligneDeCommande.Produit.Quantite)
                        {
                            ligneDeCommande.Quantite = 0;
                        }

                        if (buylist.Count == 0)
                        {
                            buylist.Add(ligneDeCommande);
                        }
                        else
                        {
                            for (int i = 0; i < buylist.Count; i++)
                            {
                                if (buylist[i].Produit.Id == ligneDeCommande.Produit.Id)
                                {
                                    if (buylist[i].Quantite + ligneDeCommande.Quantite > ligneDeCommande.Produit.Quantite)
                                    {
                                        buylist[i].Quantite = 0;
                                    }
                                    else
                                    {
                                        buylist[i].Quantite += ligneDeCommande.Quantite;
                                    }
                                    match = true;
                                    break;
                                }
                            }

                            if (!match)
                            {
                                buylist.Add(ligneDeCommande);
                            }
                        }
                    }
                }
                ViewBag.ShoppingCart = buylist;
                viewModel.ShoppingCart = buylist;
                viewModel.ShoppingCartCount = buylist.Count;
                ViewData["compteurPanier"] = viewModel.ShoppingCartCount;
                ViewData["shoppingCart"] = viewModel.ShoppingCart;

                if (viewModel.ShoppingCartCount == 0)
                {
                    ViewData.Remove("compteurPanier");
                }
            }
            RechercheProduit rechercheProduit = new RechercheProduit();
            List<Produit> produits = new List<Produit>();
            List<Produit> produitsOrdonnees = new List<Produit>();
            List<string> imagePaths = new List<string>();
            List<string> categories = _produitRepository.GetCategories();

            ViewBag.Categories = categories;
            imagePaths = _produitRepository.GetImagePaths();
            produits = _produitRepository.GetAllProduits();
            produitsOrdonnees = rechercheProduit.SortProducts(produits, "nameAsc");
            ViewData["produitsOrdonnees"] = produitsOrdonnees;
            ViewData["imagePaths"] = imagePaths;
            HttpContext.Session.Set("ShoppingCart", JsonSerializer.SerializeToUtf8Bytes(buylist));
            return View("~/Views/Home/pageAfficherListeProduits.cshtml", viewModel);
        }

        public ActionResult Checkout()
        {

            List<LigneDeCommande> buylist = new List<LigneDeCommande>();

            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData) && cartData != null)
            {
                // Deserialize the cart data from bytes to your list type
                buylist = JsonSerializer.Deserialize<List<LigneDeCommande>>(cartData);
            }
            ViewData["shoppingCart"] = buylist;
            float total = 0;

            if (buylist != null)
            {
                foreach (var anOrder in buylist)
                {
                    decimal price = anOrder.PrixUnitaire;
                    long qty = anOrder.Quantite;
                    total += (float)(price * qty);
                }
                total += total * 0.15f;
                total += 0.005f;
            }

            string amount = total.ToString("F2");

            ViewData["amount"] = amount;

            return View("~/Views/Home/pageCheckout.cshtml");
        }

        public ActionResult pageConfirmationAchat()
        {
            List<LigneDeCommande> buylist = new List<LigneDeCommande>();

            if (HttpContext.Session.TryGetValue("ShoppingCart", out byte[] cartData) && cartData != null)
            {
                // Deserialize the cart data from bytes to your list type
                buylist = JsonSerializer.Deserialize<List<LigneDeCommande>>(cartData);
            }

            int commandeID = _produitRepository.GetLastCommandeId() + 1001;
            string numero = "CMDVC"+ commandeID;

            Commande commande = new Commande();
            commande.Numero = numero;
            commande.Date = DateTime.Now;
            _produitRepository.AddCommand(commande);

            List<Ligne_commande> listeLigneCommande = new List<Ligne_commande>();

            foreach (var item in buylist)
            {
                Ligne_commande lcommande = new Ligne_commande();
                lcommande.quantite = (int)item.Quantite;
                lcommande.prix_unitaire = item.PrixUnitaire;
                lcommande.produit_id = item.Produit.Id;
                lcommande.commande_id = commandeID;
                listeLigneCommande.Add(lcommande);
             }

            _produitRepository.AddLigneCommande(listeLigneCommande);

            HttpContext.Session.Remove("ShoppingCart");
            ViewData["compteurPanier"] = 0;
            ViewData["shoppingCart"] = null;
            return View("~/Views/Home/pageConfirmationAchat.cshtml");
        }

        private LigneDeCommande GetLigneDeCommande(string id, long qty)
        {
            int productId = Convert.ToInt16(id);
            Produit produit = _produitRepository.GetProduit(productId);
            LigneDeCommande ligneDeCommande = new LigneDeCommande
            {
                Id = produit.Id,
                Produit = produit,
                Quantite = qty,
                PrixUnitaire = produit.Prix
            };

            if (qty > produit.Quantite)
            {
                ligneDeCommande.Quantite = 0;
            }

            return ligneDeCommande;
        }
    }
}
