namespace tpFinalA16.Util
{
    public class CreationListeImage
    {
        public List<string> GetAllImagePaths()
        {
            List<string> imagePaths = new List<string>();

            // Obtenir le chemin absolu du dossier "images" dans le projet
            string imagesFolderPath = "images";

            // Vérifier si le dossier "images" existe
            if (Directory.Exists(imagesFolderPath))
            {
                // Récupérer la liste des fichiers dans le dossier
                string[] files = Directory.GetFiles(imagesFolderPath);

                // Parcourir chaque fichier
                foreach (string file in files)
                {
                    // Vérifier si le fichier est une image
                    if (IsImageFile(file))
                    {
                        // Ajouter le chemin absolu du fichier à la liste
                        imagePaths.Add(file);
                    }
                }

                // Trier les chemins des images en utilisant LINQ
                imagePaths = imagePaths.OrderBy(GetImageNumber).ToList();
            }

            return imagePaths;
        }

        private bool IsImageFile(string filePath)
        {
            // Vérifier si l'extension du fichier correspond à une extension d'image courante
            string fileExtension = Path.GetExtension(filePath).ToLower();
            return fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png"
                || fileExtension == ".gif" || fileExtension == ".bmp";
        }

        private int GetImageNumber(string imagePath)
        {
            // Extraire le numéro de l'image à partir du nom de fichier
            string fileName = Path.GetFileName(imagePath);
            string[] parts = fileName.Split('-');
            if (parts.Length > 0)
            {
                if (int.TryParse(parts[0], out int imageNumber))
                {
                    return imageNumber;
                }
            }
            return 0; // Valeur par défaut si le numéro n'est pas trouvé ou invalide
        }
    }
}
