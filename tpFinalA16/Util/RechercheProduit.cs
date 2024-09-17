namespace tpFinalA16.Util
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using tpFinalA16.Models;

    namespace Utils
    {
        public class RechercheProduit
        {

            public List<Produit> SearchByName(string partialName, List<Produit> products)
            {
                return products
                    .Where(product => product.Nom.ToLower().Contains(partialName.ToLower()))
                    .ToList();
            }

            public List<Produit> SearchByCategory(string category, List<Produit> products)
            {
                return products
                    .Where(product => product.Categorie.ToLower() == category.ToLower())
                    .ToList();
            }

            public List<Produit> SearchByManufacturer(string manufacturer, List<Produit> products)
            {
                return products
                    .Where(product => product.Fabricant.ToLower() == manufacturer.ToLower())
                    .ToList();
            }

            public List<Produit> GetDiscoverProducts(List<Produit> products)
            {
                int totalProducts = products.Count;

                if (totalProducts <= 5)
                {
                    return products;
                }
                else
                {
                    Random random = new Random();
                    List<Produit> discoverProducts = products.OrderBy(p => random.Next()).Take(5).ToList();
                    return discoverProducts;
                }
            }

            public List<Produit> FilterByPriceRange(List<Produit> productList, decimal minPrice, decimal maxPrice)
            {
                return productList
                    .Where(product => (minPrice == 0 || product.Prix >= minPrice) && (maxPrice == 0 || product.Prix <= maxPrice))
                    .ToList();
            }

            public List<Produit> FilterByStartingLetter(List<Produit> productList, string startingLetter)
            {
                char letter = char.ToLower(startingLetter[0]);

                return productList
                    .Where(product => char.ToLower(product.Nom[0]) == letter)
                    .ToList();
            }

            public List<Produit> FilterByFavorites()
            {
                using (var context = new AppDbContext())
                {
                    var produitsFavoris = context.Produits.FromSqlRaw("exec FilterProductsByFavorites").ToList();

                    return produitsFavoris;
                }

            }


            public List<Produit> FilterByAvailableStock(List<Produit> products)
            {
                return products
                    .Where(product => product.Quantite > 0)
                    .ToList();
            }

            public List<Produit> SortProducts(List<Produit> sortedProducts, string sortBy)
            {
                if (sortBy.Equals("nameAsc"))
                {
                    return sortedProducts.OrderBy(product => product.Nom).ToList();
                }
                else if (sortBy.Equals("nameDesc"))
                {
                    return sortedProducts.OrderByDescending(product => product.Nom).ToList();
                }
                else if (sortBy.Equals("priceAsc"))
                {
                    return sortedProducts.OrderBy(product => product.Prix).ToList();
                }
                else if (sortBy.Equals("priceDesc"))
                {
                    return sortedProducts.OrderByDescending(product => product.Prix).ToList();
                }
                return sortedProducts;
            }
        }
    } 

}
