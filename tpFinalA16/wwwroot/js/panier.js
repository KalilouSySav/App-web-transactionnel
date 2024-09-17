
// Récupérez le bouton du panier et les détails du panier
var panierButton = document.getElementById('panier-button');
var panierDetails = document.getElementById('panier-details');

// Vérifiez si le bouton du panier et les détails du panier existent
if (panierButton && panierDetails) {
    // Ajoutez un écouteur d'événement pour le clic sur le bouton du panier
    panierButton.addEventListener('click', function () {
        // Vérifiez si les détails du panier sont visibles
        var isVisible = panierDetails.style.display === 'block';

        // Basculez l'affichage des détails du panier en utilisant les propriétés CSS
        panierDetails.style.display = isVisible ? 'none' : 'block';

    });

}