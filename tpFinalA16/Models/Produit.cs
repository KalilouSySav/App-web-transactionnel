namespace tpFinalA16.Models
{
    public class Produit
    {
        private int id;
        private string nom;
        private string description;
        private string categorie;
        private string fabricant;
        private decimal prix;
        private int quantite;
        private bool favoris;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public string Categorie
        {
            get { return categorie; }
            set { categorie = value; }
        }

        public string Fabricant
        {
            get { return fabricant; }
            set { fabricant = value; }
        }

        public decimal Prix
        {
            get { return prix; }
            set { prix = value; }
        }

        public int Quantite
        {
            get { return quantite; }
            set { quantite = value; }
        }

        public bool Favoris
        {
            get { return favoris; }
            set { favoris = value; }
        }
    }

}
