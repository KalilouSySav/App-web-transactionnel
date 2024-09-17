namespace tpFinalA16.Models
{
    public class LigneDeCommande
    {
        public long Id { get; set; }
        public Produit Produit { get; set; }
        public long Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }
    }
}
