namespace tpFinalA16.Models
{
    public class Ligne_commande
    {
        public int id { get; set; }
        public int quantite { get; set; }
        public decimal prix_unitaire { get; set; }
        public int commande_id { get; set; }
        public int produit_id { get; set; }

    }
}
