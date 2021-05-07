# dotnet_project_MTI2022
Le but de notre projet est de réaliser une application web : FripShop. Qui vise à mettre en relation des
utilisateurs souhaitant vendre des vêtements d’occasion.

FripShop permet à tous les particuliers de mettre en vente des vêtements ou des accessoires, sur un
modèle similaire à celui de Leboncoin. En effet, il en est de la responsabilité des deux parties (vendeur
et acheteur) de s’accorder sur le déroulement de la transaction.

Il est extrêmement simple de mettre en vente un de ses vêtements. Pour cela, il suffit d’ajouter une
photo ainsi qu’une description du produit, puis de déterminer un prix. Lorsqu’un utilisateur est
intéressé par un article, il peut le réserver et entrer en contact avec le vendeur.

Chaque vendeur est noté vis à vis de son honnêteté ainsi qu’à la qualité de l’annonce postée sur
l’application. Suite à chaque vente, le client peut noter le vendeur. La note globale d’un vendeur (ainsi
que le nombre total de notes) sera affichée lors du processus d’achat, afin d’informer chaque client de
la confiance qu’il peut accorder au vendeur.


--- INSTALLATION DE L'APPLICATION ---

Installer https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer pour IIS

Deployer la db grâce au script:
Ouvrir SSMS, fichier->ouvrir->fichier puis ouvrir, puis éxecuter

Déployer l'application sur IIS:
Ouvrir IIS
Dans Connexion
Dérouler DESKTOP-XXXXX ou LAPTOP-XXXXX
Bouton droit sur "Sites"
Ajouter un site Web
Nom du site: "FripShop", Pool d'application: "FripShop"
Dans le chemin d'accès physique, selectionner C:/inetpub/wwwroot puis créer un nouveau dossier "FripShop" et le selectionner
Se connecter en tant que
Utilisateur de l'application (Authentification directe)
port: 8080
Bouton droit sur le site FripShop nouvellement créé
modifier les autorisations
Sécurité->Modifier->Ajouter->Avancé
Rechercher puis sélectionner le groupe "Tout le monde"->OK->OK->puis cocher la case "contôle total" de la colonne "Autoriser"
Revenir sur IIS
Dans la colonne "Action"->paramètres de base->Se connecter en tant que->Utilisateur spécifique->Définir
Se connecter avec le compte utilisateur de la session actuelle->OK->OK
Dans le dossier source du projet->modifier le fichier FripShop/appsettings.json remplir la connection string de la database précedemment créée
Ouvrir la solution avec VisualStudio 2019 en mode administrateur,
Dans le fichier FripShop/DataAccess/EFModels/FripShopContext.cs remplir la connection string de la database précedemment créée
Bouton droit sur le projet "FripShop"->Publier->Selectionner serveur Web IIS->Web Deploy
Server: "localhost"
Nom du site : "FripShop"
url : "localhost:8080"
Terminer-> sur le profile IIS selectionner "Edition"->Suivant->Dans base de donnée, cocher "Utiliser cette chaine au moment de l'exécution"->Enregister
Ouvrir l'onglet "Services Connectés"->A côté de "base de donnée de serveur SQL"->Configurer->Base de donnée de serveur SQL->Valeur de la chaine de connexion faire parcourir->
Nom du server, ajouter le nom du server SQL server->Authentification, selectionner "Authentification windows"->Dans connexion à la base de donnée, selectionner "fripshop"->OK->Suivant->Terminer->Fermer
Revenir sur l'onglet "Publier"->Publier
Revenir sur IIS, dans "Pool d'application"->FripShop->Paramètres Avancés->Identité->Séléctionner compte personnalisé puis se connecter avec les identifiants de la session utilisateur actuelle
Dans un navigateur vous pouvez désormais vous connecter à l'addresse localhost:8080


--- ENDPOINTS API ---

Article controller:
- "/api/articles/" : GET : PUBLIC : Get tous les articles présents dans la DB, retourne un JSON contenant la liste des articles avec leur vendeur respectif
- "/api/articles/{articleId}" : GET : PUBLIC : Get un article, retourne un JSON avec l'article dedans
- "/api/articles/{articleId}/getUser" : GET : PUBLIC : Get le user PUBLIC d'un article, retourne un JSON avec le user PUBLIC

User controller:
- "/api/users/" : GET : Get tous les users avec leurs informations publiques
- "/api/users/{userId}" : GET : Get les informations publiques d'un User, retourne un UserPublic (JSON)
- "/api/users/{userId}/getArticles" : GET : Get pour récupérer tous les articles mis en vente d'un User