namespace tpFinalA16.Models
{
    public class ShoppingCartViewModel
    {
        public List<LigneDeCommande> ShoppingCart { get; set; }
        public int ShoppingCartCount { get; set; }
        public float TotalAmount { get; set; }
    }
}
