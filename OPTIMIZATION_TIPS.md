Notes d’optimisation

Au niveau de la physique :

-  Optimisation de la matrice de collisions en supprimant les collisions qui ne sont pas importantes ou qui n’ont aucune utilité dans la scène, par exemple les collisions entre projectiles.

-  Utilisation de colliders primitif (Box, Capsule, Sphere, etc.) pour éviter le calcul des formes complexs, qui peut également conduire à un bottleneck.

Au niveau du lighting :

-  Utilisation de lumières statiques dans les espaces où les lumières dynamiques ne sont pas vraiment utiles.

-  Utilisation du light baking pour les environnements statiques afin d’éliminer le calcul de l’éclairage en temps réel.

au Niveau Code : 
- Mise en cache des références (Caching) : Évitez d’appeler GetComponent<T>(), Camera.main ou GameObject.Find() à l’intérieur des méthodes Update() ou de tir. Récupérez ces composants une seule fois dans Awake() ou   Start(), puis stockez-les dans des variables privées.

- Zéro allocation dans Update : Ne créez jamais de nouveaux objets tels que new Class() ou des instanciations de chaînes de caractères à chaque frame. Utilisation le pooling d'objets qui perement une reutilisation    des object en réinitialisant leur  attribut 

- Optimisation de la Logique et des Boucles Update : Tous les scripts n’ont pas besoin de s’exécuter 60 ou 100 fois par seconde.
- Événements plutôt que Update permanent : Si un script doit réagir à un événement tel qu’un dégât subi ou un niveau réussi, utilisation d'un evement reste le meilleur choix
 au niveau assets :
- Utilisation d'asset optimiser avec le minimum de polugone possible
- reduire l'utilisation de shader et vfx trop gourmand au niveau des ressouces 
