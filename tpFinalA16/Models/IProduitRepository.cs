using tpFinalA16.Util;
using tpFinalA16.Util.Utils;

namespace tpFinalA16.Models
{
    public interface IProduitRepository
    {
        List<Produit> GetProduitsDecouverts();
        List<string> GetImagePaths();
        List<string> GetCategories();
        List<Produit> GetAllProduits();
        Produit GetProduit(int id);
        int GetLastCommandeId();
        void AddCommand(Commande commande);
        void AddLigneCommande(List<Ligne_commande> listeLigneCommande);
    }

    public class ProduitRepository : IProduitRepository
    {
        private readonly AppDbContext _context;

        public ProduitRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Produit> GetProduitsDecouverts()
        {
            RechercheProduit rechercheProduit = new RechercheProduit();
            List<Produit> produits = _context.Produits.ToList();
            List<Produit> produitsDecouverts = rechercheProduit.GetDiscoverProducts(produits);
            return produitsDecouverts;
        }

        public List<string> GetImagePaths()
        {
            CreationListeImage creationListeImage = new CreationListeImage();
            List<string> imagePaths = new List<string>();
            imagePaths = creationListeImage.GetAllImagePaths();
            return imagePaths;
        }

        public List<string> GetCategories()
        {
            return _context.Produits.Select(p => p.Categorie).Distinct().ToList();
        }

        public List<Produit> GetAllProduits()
        {
            return _context.Produits.ToList();
        }
        public Produit GetProduit(int id)
        {
            return _context.Produits.FirstOrDefault(p => p.Id == id);
        }

        public int GetLastCommandeId()
        {
            var lastCommandeId = _context.Commandes.OrderByDescending(c => c.Id).Select(c => c.Id).FirstOrDefault();
            return lastCommandeId;
        }

        public void AddCommand(Commande commande)
        {
            try
            {
                if (commande == null)
                {
                    throw new ArgumentNullException(nameof(commande));
                }

                _context.Commandes.Add(commande);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Une erreur s'est produite lors de l'ajout de la commande : " + ex.Message);

                throw;
            }
        }

        public void AddLigneCommande(List<Ligne_commande> listeLigneCommande)
        {
            foreach (var ligneCommande in listeLigneCommande)
            {
                _context.Ligne_commandes.Add(ligneCommande);
                _context.SaveChanges();
            }

        }
    }
}
