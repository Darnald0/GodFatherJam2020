Pour ajouter du song :

Mettre le script SoundManager dans la scene.

Dans l'inspector du script ajouter les musique que vous voulez en sélectionnant bien a quoi sert ce son. (ATTENTION LES DOUBLONS SERRONT PAS PRIS EN COMPTE)
Pour ajouter d'autre choix aller dans le script Sounds et ajouter ce que vous voulez dans l'enum.

Pour choisir un son ou plusieurs son à jouer depuis un Objet, ajouter le script PlayOneSound ou bien PlayMultipleSound,
ensuite dans l'inspector selectionner qu'elle type de son vous voulez faire jouer.

Pour jouer le son, au moment ou vous voulez jouez le son, faites reférence au script choisie avant puis utiliser la méthodes PlaySound().
Si vous avez choisie le PlayMultipleSound ajouter votre choix de type de son que vous voulez jouer a ce moment. Exemple : PlaySound(TYPE_AUDIO::MusiqueMenuDemarrageDuJeu);