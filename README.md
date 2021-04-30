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

--- ENDPOINTS API ---

Article controller:
- "/api/articles/" : GET : PUBLIC : Get tous les articles présents dans la DB, retourne un JSON contenant la liste des articles avec leur vendeur respectif
- "/api/articles/{articleId}" : GET : PUBLIC : Get un article, retourne un JSON avec l'article dedans
- "/api/articles/{articleId}/getUser" : GET : PUBLIC : Get le user PUBLIC d'un article, retourne un JSON avec le user PUBLIC
- "/api/articles/create" : POST : PRIVATE : Poste un nouvel article avec l'objet en body (JSON)

User controller:
- "/api/users/register" : POST : PRIVATE : Post pour enregistrer un nouveau User, avec le User en body (JSON). Renvoie le user créé.
- "/api/users/{userId}" : GET : PRIVATE : Get pour choper les informations privées d'un user. Retourne l'objet User (JSON)
- "/api/users/{userId}/public" : GET : PUBLIC : Get les informations publiques d'un User, retourne un UserPublic (JSON)
- "/api/users/editUser" : POST : PRIVATE : Post pour éditer un user déjà existant, avec les nouvelles infos du user dans l'objet (JSON)
- "/api/users/delete" : POST : PRIVATE : Post pour supprimer un User, avec dans le body l'ID du User.
- "/api/users/{userId}/getArticles" : GET : PUBLIC : Get pour récupérer tous les articles mis en vente d'un User