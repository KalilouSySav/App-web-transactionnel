
// R�cup�rez le bouton du panier et les d�tails du panier
var panierButton = document.getElementById('panier-button');
var panierDetails = document.getElementById('panier-details');

// V�rifiez si le bouton du panier et les d�tails du panier existent
if (panierButton && panierDetails) {
    // Ajoutez un �couteur d'�v�nement pour le clic sur le bouton du panier
    panierButton.addEventListener('click', function () {
        // V�rifiez si les d�tails du panier sont visibles
        var isVisible = panierDetails.style.display === 'block';

        // Basculez l'affichage des d�tails du panier en utilisant les propri�t�s CSS
        panierDetails.style.display = isVisible ? 'none' : 'block';

    });

}