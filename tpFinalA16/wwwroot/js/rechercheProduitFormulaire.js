
document.getElementById("voirPlusBtn").addEventListener("click", function () {
    var voirPlusContainer = document.getElementById("voirPlusContainer");
    var voirPlusBtn = document.getElementById("voirPlusBtn");

    if (voirPlusContainer.style.display === "none") {
        voirPlusContainer.style.display = "block";
        voirPlusBtn.textContent = "Voir moins";
    } else {
        voirPlusContainer.style.display = "none";
        voirPlusBtn.textContent = "Voir plus";
    }
});