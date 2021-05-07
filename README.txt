Installer https://dotnet.microsoft.com/permalink/dotnetcore-current-windows-runtime-bundle-installer sur IIS

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