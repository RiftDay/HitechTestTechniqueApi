
Ce projet a été proposé en tant que test technique à la suite de notre entretien mené le 24/01.

La remise à niveau technique, la configuration de l’environnement et l’implémentation ont été menéees en parallèle d’une semaine de travail pleine (avec heures supplémentaires), je m’y suis pleinement investi néanmoins et suis parvenu à une solution qui me satisfait compte-tenu de ces contraintes.

# Objectifs

Mettre en place une API de gestion des dépenses.

# Implémentation

## Modèle

Champs obligatoires :
- `Id` UUID auto-généré à la création
- `Montant` Valeur réélle positive non nulle, pas de borne supérieure
- `Date` Pas de contrôle sur ce champ tant que la date est valide
- `Commentaire` Non vide, longueur max. 100 caractères
- `Type` Nature de la dépense : `Deplacement` ou `Restaurant`

Champs conditionnels :
- `DistanceKm` Valeur entière positive non nulle, obligatoire si dépense `Deplacement`
- `NombresInvites` Valeur entière positive ou nulle, obligatoire si dépense `Restaurant`

Séparation de la validation dans un attribut dédié.

## Endpoints

- `GET` `/depenses/?numPage=1&nbreElemsPage=5` Récupération de la liste des dépenses avec gestion de la pagination
- `GET` `/depenses/{id}` Récupération d’une dépense spécifique à partir de son ID (UUID)
- `POST` `/depenses/` Ajout d’une nouvelle dépense avec validation
- `DELETE` `/depenses/{id}` Suppression d’une dépense spécifique à partir de son ID (UUID)

# Architecture

La solution a été implémentée en C# en utilisant .NET 9.

Elle est scindée en 2 projets :
- `HitechTestTechnique.Lib` pour les modèles
- `HitechTestTechnique.Api` pour l’API

## Base de données

**Entity Framework** a été utilisé, avec une gestion *InMemory* pour la première version, puis **PostgreSQL** une fois les problèmes de configuration de l’environnement local résolus.

J’ai utilisé l’approche *Code-First* d’**EF**, avec une seule migration créée et déployée, celle d’initialisation de la base.

La chaîne de connexion est dans le `appsettings.json` du projet API, elle suppose une instance du SGBD sur `localhost:5432` sur laquelle l’utilisateur `hitechsoftware` (mot de passse `test_technique`) a des droits en création de bases.


## Spécificités

Swagger ayant été retiré de .NET depuis la version 9, plutôt que de le rajouter, j’ai découvert et configuré une autre solution qui s’appuie sur l’OpenAPI, `Scalar`.

J’ai configuré le projet pour que cette interface soit active même en production, dans le cadre de projet.


# Améliorations

Si plus de temps m’était disponible, voici les améliorations que j’aurais voulu mettre en place :
- Mise en place d’un mécanisme de journalisation (utilisation probable de `Serilog`)
- Meilleure validation :
    - Messages d’erreur sur champs correspondant à des propriétés `required`
    - Gestion des remontées multiples d’erreur de validation
- Gestion de l’authentification et des autorisations
- Mise en place d’un petit projet pour consommer l’API
- Pas de `<summary>` jugé nécessaire pour les champs du modèle, compte-tenu de leur simplicité

# Livrable

En plus du dépôt présent, la version *InMemory* du projet a été déployée sur un serveur de type Debian.
Une courte vidéo est fournie, servant à présenter l’API en fonctionnement local, ainsi que l’environnement de développement utilisé.

Les liens vers ces deux ressources vous ont été fournies par mail (et resteront disponibles jusqu’à fin février).

Merci pour votre temps.
